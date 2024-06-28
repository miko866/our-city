using Microsoft.Extensions.Diagnostics.HealthChecks;
using Server.Common;
using Server.GraphQL;
using Server.GraphQL.Mutations;
using Server.GraphQL.Queries;
using Server.Helpers;
using Server.Security;
using Server.Services;
using Server.Utils;
using Query = Server.GraphQL.Queries.Query;

namespace Server.Config;

public static class CustomServicesConfig
{
    /// <summary>
    /// Customer Service collection for business logic.
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IFileItemCommon, FileItemCommon>();
        services.AddTransient<IGisCommon, GisCommon>();
        services.AddTransient<IJwtGenerator, JwtGenerator>();

        services.AddTransient<IMobileAppService, MobileAppService>();
        services.AddTransient<IModuleEventService, ModuleEventService>();
        services.AddTransient<IModuleMunicipalRadioService, ModuleMunicipalRadioService>();
        services.AddTransient<IModuleSpecialAnnouncementService, ModuleSpecialAnnouncementService>();
        services.AddTransient<IModuleNewsService, ModuleNewsService>();
        services.AddTransient<IModuleSimplePageService, ModuleSimplePageService>();
        services.AddTransient<IOrganisationService, OrganisationService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<ISeederService, SeederService>();
        services.AddTransient<IUserService, UserService>();

        services.AddTransient<IErrorMessages, ErrorMessages>();
    }

    /// <summary>
    /// GraphQL HotChocolate setup
    /// </summary>
    /// <param name="services"></param>
    /// <param name="environment"></param>
    public static void AddCustomGraphqlServices(this IServiceCollection services, IHostEnvironment environment)
    {
        services
            .AddGraphQLServer()
            // .AddCacheControl()
            // .ModifyCacheControlOptions(o => o.DefaultMaxAge = 60_000)
            // .UseQueryCachePipeline()
            .ModifyRequestOptions(o =>
            {
                // 👇 Include Exception Details for Debugging
                o.IncludeExceptionDetails = EnvironmentUtil.AllowDebugForEnvironments(environment.EnvironmentName);
                o.Complexity.Enable = true;
                o.Complexity.MaximumAllowed = 250;
                o.ExecutionTimeout = TimeSpan.FromSeconds(60);
            })
            .SetRequestOptions(_ => new HotChocolate.Execution.Options.RequestExecutorOptions
            {
                ExecutionTimeout = TimeSpan.FromMinutes(2)
            })
            .AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>()
            .AddAuthorization()
            .AddFiltering()
            .AddProjections()
            .AddSorting()
            .AllowIntrospection(EnvironmentUtil.AllowDebugForEnvironments(environment.EnvironmentName))
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddTypeConverter<DateTimeOffset, DateTime>(t => t.LocalDateTime)
            .AddMaxExecutionDepthRule(6, skipIntrospectionFields: true)
            .SetMaxAllowedValidationErrors(10);
    }

    /// <summary>
    /// AddHealthCheckServices
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddHealthCheckServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(
                configuration["ConnectionStrings:Database"]!,
                healthQuery: "SELECT 1;",
                name: "our-city-db",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "db", "postgresql", "our-city" }
            );
    }
}
