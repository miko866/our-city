using System.Globalization;
using Blazored.LocalStorage;
using Client;
using Client.Middlewares;
using Client.Services;
using Client.Services.GraphQLServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddSingleton<AuthTokenService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

builder.Services.AddTransient<AuthenticationHandler>();

builder
    .Services.AddGraphQLClient()
    .ConfigureHttpClient(
        client =>
        {
            client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}graphql");
            client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);
        },
        clientBuilder => clientBuilder.AddHttpMessageHandler<AuthenticationHandler>()
    );

// Add custom services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddSingleton<ISnackbar, SnackbarService>();
builder.Services.AddSingleton<ILoadingIndicator, LoadingIndicator>();

CultureInfo.CurrentCulture = new CultureInfo("sk-SK");
CultureInfo.CurrentUICulture = new CultureInfo("sk-SK");
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("sk-SK");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("sk-SK");

if (builder.HostEnvironment.IsProduction())
{
    builder.Logging.SetMinimumLevel(LogLevel.None);
}

await builder.Build().RunAsync();
