using System.Threading.RateLimiting;
using Data;
using HealthChecks.UI.Client;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using Server.AutoMapper;
using Server.Config;
using Server.Config.SwaggerConfigs;
using Server.GraphQL;
using Server.Helpers;
using Server.Security;
using Server.Utils;
using Path = System.IO.Path;

Logger logger = null!;
Console.WriteLine("Init debugger.");

List<string> validEnvironments =
[
    Constants.Environments.LocalDevelopment,
    Constants.Environments.LocalIntegrationTest,
    Constants.Environments.Development,
    Constants.Environments.Testing,
];

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    string envName = builder.Environment.EnvironmentName;

    // Check if the provided environment is valid
    // Setup nLogger config depends on env variables
    if (validEnvironments.Contains(envName))
    {
        string fileName = $"nlog.{envName}.config";
        logger = LogManager.Setup().LoadConfigurationFromFile(fileName).GetCurrentClassLogger();
        logger.Debug("Current Environment: " + builder.Environment.EnvironmentName);
    }
    else
    {
        Console.WriteLine(
            "No valid environment given, aborting application. Use 'export ASPNETCORE_ENVIRONMENT=<EnvName>'"
        );
        Thread.Sleep(5 * 60 * 1000);
        Environment.Exit(0);
    }

    // https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-7.0
    builder
        .Services.AddDataProtection()
        .SetApplicationName("OurCity")
        .PersistKeysToDbContext<ApplicationDbContext>()
        .SetDefaultKeyLifetime(TimeSpan.FromDays(1826)) // 5 years
        .UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration()
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
            }
        );

    // Check if all values in appsettings.json are set up
    builder.Services.AddAppSettingsValidation();

    builder.Services.TestConnectionEf(builder.Configuration, builder.Environment.EnvironmentName, logger);

    // Remove SERVER from response header
    builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
    builder.WebHost.UseStaticWebAssets();

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddResponseCaching();
    builder.Services.AddCustomLocalization(builder.Configuration);
    builder.Services.AddCustomSwagger();
    builder.Services.AddHealthCheckServices(builder.Configuration);

    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = 429;
        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                factory: partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 1_200,
                    QueueLimit = 12,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    Window = TimeSpan.FromMinutes(1)
                }
            )
        );
    });

    // For Async IO Operations
    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.AllowSynchronousIO = true;
        options.MaxRequestBodySize = int.MaxValue;
    });
    builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.AllowSynchronousIO = true;
        options.Limits.MaxRequestBodySize = int.MaxValue; // if not set default value is: 30 MB
    });

    // Size limit for multipart-Forms.
    builder.Services.Configure<FormOptions>(x =>
    {
        x.ValueLengthLimit = int.MaxValue;
        x.MultipartBodyLengthLimit = int.MaxValue; // if not set default value is: 128 MB
        x.MultipartHeadersLengthLimit = int.MaxValue;
    });

    // https://stackoverflow.com/questions/69961449/net6-and-datetime-problem-cannot-write-datetime-with-kind-utc-to-postgresql-ty
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    builder.Services.AddDbContext<ApplicationDbContext>(
        optionsAction: options =>
        {
            options.UseNpgsql(builder.Configuration["ConnectionStrings:Database"]);

            if (envName == Constants.Environments.LocalDevelopment)
            {
                options.EnableDetailedErrors(); // To get field level error details
                options.EnableSensitiveDataLogging(); // To get parameter values
                options.ConfigureWarnings(warningsAction =>
                {
                    warningsAction.Log(
                        [
                            CoreEventId.FirstWithoutOrderByAndFilterWarning,
                            CoreEventId.RowLimitingOperationWithoutOrderByWarning
                        ]
                    );
                });
            }
        },
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Singleton
    );

    // Access to HTTP
    builder.Services.AddHttpContextAccessor();
    // For HTTP Requests (Get, Post ...)
    builder.Services.AddHttpClient();

    // Add Services
    builder.Services.AddCustomAuthentication(builder.Configuration, builder.Environment);
    builder.Services.AddCustomAuthorization();
    builder.Services.RateLimiter();
    builder.Services.AddTransient<ApplicationDbContext, ApplicationDbContext>();
    builder.Services.AddScoped<JwtGenerator>();
    builder.Services.AddCustomIdentityAuth();
    builder.Services.AddAutoMapper(typeof(AutoMapperFactory).Assembly);
    builder.Services.AddCustomServices();

    // Global GraphQL Error handler
    builder.Services.AddErrorFilter<GraphQlErrorFilter>();

    // HotChocolate GraphQL
    builder.Services.AddCustomGraphqlServices(builder.Environment);

    // Custom asp net core token expired time -> Reset Email token or reset Password token
    // Default is 1 Day
    builder.Services.Configure<DataProtectionTokenProviderOptions>(x => x.TokenLifespan = TimeSpan.FromHours(24));

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // CORS setup
    switch (envName)
    {
        case Constants.Environments.LocalDevelopment:
        case Constants.Environments.Development:
        case Constants.Environments.Testing:
        case Constants.Environments.LocalIntegrationTest:
            builder.Services.CorsOriginsAllowAll();
            break;
        case Constants.Environments.Staging:
        case Constants.Environments.Production:
            builder.Services.CorsOriginsRestrictByConfigFile(builder.Configuration);
            break;
        default:
            logger.Info("No Cors config found, stopping application");
            Environment.Exit(0);
            break;
    }

    // Build the application.
    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline for custom Environments.
    if (EnvironmentUtil.AllowDebugForEnvironments(envName))
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // Allow multi-language
    app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

    app.UseSwaggerAuthorized();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseForwardedHeaders();
    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    string filepath = envName.Equals(Constants.Environments.LocalIntegrationTest)
        ? "../../../../storage/"
        : "../storage/";
    string fullPath = Path.GetFullPath(filepath);
    app.UseStaticFiles(
        new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), fullPath)),
            RequestPath = "/storage",
        }
    );

    app.UseRouting();
    app.UseRateLimiter();
    app.UseCors(EnvironmentUtil.AllowDebugForEnvironments(envName) ? "AllowAllCors" : "AllowGraphQlCors");
    app.UseResponseCaching();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGraphQL()
        .WithOptions(
            new GraphQLServerOptions
            {
                Tool =
                {
                    Enable =
                        app.Environment.IsDevelopment()
                        || EnvironmentUtil.AllowDebugForEnvironments(app.Environment.EnvironmentName)
                },
                EnableGetRequests = true
            }
        )
        .RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme })
        .AllowAnonymous();

    app.MapRazorPages();
    app.MapControllers()
        .RequireCors(EnvironmentUtil.AllowDebugForEnvironments(envName) ? "AllowAllCors" : "AllowControllersCors");
    app.MapFallbackToFile("index.html");

    app.MapHealthChecks(
        "_health",
        new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
    );

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}

// Create partial class for xUnit Testing
public partial class Program { }
