namespace Server.Config;

public static class CorsConfig
{
    /// <summary>
    /// CORS allow all only for LocalDevelopment
    /// </summary>
    /// <param name="services"></param>
    public static void CorsOriginsAllowAll(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy(
                "AllowAllCors",
                builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition")
            )
        );
    }

    /// <summary>
    /// CORS settings for other env.
    /// GraphQL and REST-API settings are different
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void CorsOriginsRestrictByConfigFile(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
            options.AddPolicy(
                "AllowGraphQlCors",
                builder =>
                    builder
                        .WithOrigins(configuration["Urls:Client"]!)
                        .WithMethods("POST")
                        .AllowCredentials()
                        .AllowAnyHeader()
            )
        );

        services.AddCors(options =>
            options.AddPolicy(
                "AllowControllersCors",
                builder =>
                    builder
                        .WithOrigins(configuration["Urls:Client"]!)
                        .WithMethods("POST, GET, PUT, PATCH, DELETE, OPTIONS, HEAD")
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition")
            )
        );
    }
}
