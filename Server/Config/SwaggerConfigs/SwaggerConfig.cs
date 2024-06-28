using Microsoft.OpenApi.Models;

namespace Server.Config.SwaggerConfigs;

public static class SwaggerConfig
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        // services.AddSwaggerGen();
        services.AddSwaggerGen(options =>
        {
            // Docs information for Swagger
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Our City API",
                    Description = "API Description",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "SuperParta",
                        Url = new Uri("http://codingsonata.com/contact"),
                        Email = "aram@codingsonata.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                }
            );

            // Security endpoints for Swagger -> JWT token
            options.AddSecurityDefinition(
                "JWT Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "This is a JWT bearer authentication scheme",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Id = "JWT Bearer", Type = ReferenceType.SecurityScheme }
                        },
                        new List<string>()
                    }
                }
            );

            // Request Header language for Swagger
            options.OperationFilter<AcceptLanguageOperationFilter>();
        });
    }
}
