using Microsoft.Extensions.Options;
using Server.Models.AppSettings;

namespace Server.Config;

public static class AppSettingsConfig
{
    /// <summary>
    /// Check and validate appsettings.json values
    /// </summary>
    /// <param name="services"></param>
    public static void AddAppSettingsValidation(this IServiceCollection services)
    {
        services
            .AddOptions<ConnectionStringsSettings>()
            .BindConfiguration("ConnectionStrings") // Bind the SwaggerAuth section
            .ValidateDataAnnotations() // Enable validation
            .ValidateOnStart(); // Validate on app start

        services
            .AddOptions<EmailConfigurationSettings>()
            .BindConfiguration("EmailConfiguration")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<FileSettings>().BindConfiguration("File").ValidateDataAnnotations().ValidateOnStart();

        services
            .AddOptions<FileStorageSettings>()
            .BindConfiguration("FileStorage")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<JwtOptionsSettings>()
            .BindConfiguration("JwtOptions")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<LanguageSettings>()
            .BindConfiguration("Language")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<MobileAppTokenSettings>()
            .BindConfiguration("MobileAppToken")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<SwaggerAuthSettings>()
            .BindConfiguration("SwaggerAuth")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<UrlsSettings>().BindConfiguration("Urls").ValidateDataAnnotations().ValidateOnStart();

        // Explicitly register the settings object by delegating to the IOptions object
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ConnectionStringsSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EmailConfigurationSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<FileSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<FileStorageSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtOptionsSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<LanguageSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MobileAppTokenSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SwaggerAuthSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<UrlsSettings>>().Value);
    }
}
