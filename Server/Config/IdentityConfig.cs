using Data;
using Data.Entities;
using HotChocolate.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using Server.Security;
using Shared.Extensions;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Config;

public static class IdentityConfig
{
    /// <summary>
    /// Add custom identity with extended setup
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomIdentityAuth(this IServiceCollection services)
    {
        // Added Identity
        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password configs
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            // options.Password.RequiredUniqueChars = 6;

            // Lockout settings
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 6;

            // ApplicationUser settings
            // options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_ßöäüÄÖÜÀàÂâÆæÇçÉéÈèÊêËëÎîÏïÔôŒœÙùÛûÜüŸÿìíòóÁÄČĎÉÍĹĽŇÓÔŔŠŤÚÝŽáäčďéíĺľňóôŕšťúýžĚŘŮěřů";
        });
    }

    /// <summary>
    /// Added Bearer JWT-Token Authentication
    /// Verify JWT Token
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="environment"></param>
    public static void AddCustomAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment
    )
    {
        // .NET 8 breaking change
        // Remove default obsolete claims
        Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

        // Configure how the Bearer token is validated
        var signingKey = new SymmetricSecurityKey(
            System.Text.Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecretKey").Value!)
        );

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.IncludeErrorDetails = true; // For debugging

                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,
                    ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.Zero,
                };

                // Validate token
                configureOptions.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        string userId = context.Principal!.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!;

                        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

                        ApplicationUser existingUser = dbContext
                            .ApplicationUser?.Where(u => u.Id == userId)
                            .FirstOrDefault()!;

                        if (existingUser.IsNull())
                        {
                            var configurationService =
                                context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                            string subValue = configurationService.GetSection("MobileAppToken:Sub").Value!;

                            if (userId.Equals(subValue))
                                return Task.CompletedTask;

                            context.Fail(ErrorCodes.CODE_AUTHORIZATION_ERROR_CANNOT_LOGGIN);
                        }
                        return Task.CompletedTask;
                    },
                };

                if (environment.EnvironmentName == Constants.Environments.LocalDevelopment)
                {
                    configureOptions.RequireHttpsMetadata = false;
                }
            });
    }

    /// <summary>
    /// AddCustomAuthorization
    /// Role base authorization
    /// Here adds your customs role names
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Constants.Policies.IsSysAdmin,
                policy =>
                    policy.RequireAssertion(context => context.User.IsUserRole(Shared.Helpers.Constants.Roles.Admin))
            );

            // TODO: Adds multitenant roles and policies and permissions

            // options.AddPolicy(
            //     Constants.Policies.IsOrganizationAdmin,
            //     policy =>
            //         policy.RequireAssertion(context =>
            //             context.User.IsUserRole(Constants.Roles.SysAdmin)
            //             || context.User.IsUserRole(Constants.Roles.OrganizationAdmin)
            //         )
            // );
            //
            // options.AddPolicy(
            //     Constants.Policies.IsProfileOwner,
            //     policy =>
            //         policy.RequireAssertion(context =>
            //             context.User.IsUserRole(Constants.Roles.SysAdmin)
            //             || context.User.IsUserRole(Constants.Roles.OrganizationAdmin)
            //             || context.User.IsUserRole(Constants.Roles.ProfileOwner)
            //         )
            // );
            //
            // options.AddPolicy(
            //     Constants.Policies.IsProfileAdmin,
            //     policy =>
            //         policy.RequireAssertion(context =>
            //             context.User.IsUserRole(Constants.Roles.SysAdmin)
            //             || context.User.IsUserRole(Constants.Roles.OrganizationAdmin)
            //             || context.User.IsUserRole(Constants.Roles.ProfileOwner)
            //             || context.User.IsUserRole(Constants.Roles.ProfileAdmin)
            //         )
            // );
            //
            // options.AddPolicy(
            //     Constants.Policies.IsSupervisor,
            //     policy =>
            //         policy.RequireAssertion(context =>
            //             context.User.IsUserRole(Constants.Roles.Supervisor)
            //             || context.User.IsUserRole(Constants.Roles.OrganizationAdmin)
            //             || context.User.IsUserRole(Constants.Roles.SysAdmin)
            //         )
            // );
        });
    }
}
