using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Server.Config;

public static class LocalizationConfig
{
    /// <summary>
    /// AddCustomLocalization
    /// Multi language support
    /// https://azuliadesigns.com/c-sharp-tutorials/list-net-culture-country-codes/
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddCustomLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization();

        string languageSk = configuration.GetSection("Language:SupportedLanguages").Get<string[]>()![0];
        // ReSharper disable once InconsistentNaming
        string languageSkSK = configuration.GetSection("Language:SupportedLanguages").Get<string[]>()![1];

        var cultureSk = new CultureInfo(languageSk);
        // ReSharper disable once InconsistentNaming
        var cultureSkSK = new CultureInfo(languageSkSK);

        services.Configure<RequestLocalizationOptions>(options =>
        {
            CultureInfo[] supportedCultures = { cultureSk, cultureSkSK };

            options.DefaultRequestCulture = new RequestCulture(culture: cultureSkSK, uiCulture: cultureSkSK);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.ApplyCurrentCultureToResponseHeaders = true;
        });
    }
}
