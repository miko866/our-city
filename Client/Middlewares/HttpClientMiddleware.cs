using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Client.Helpers;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Middlewares;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly AuthTokenService _authTokenService;
    private readonly NavigationManager _navigationManager;

    public AuthenticationHandler(AuthTokenService authTokenService, NavigationManager navigationManager)
    {
        _authTokenService = authTokenService;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (!request.Headers.Contains("Authorization"))
        {
            string token = _authTokenService.GetAuthToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (!string.IsNullOrEmpty(token))
            {
                string jwtEncoded = token.Split('.')[1];
                byte[] jwtDecoded = JwtHelper.ParseBase64WithoutPadding(jwtEncoded);
                var payloadJson = JsonSerializer.Deserialize<JsonElement>(jwtDecoded);

                // Extract the expiration claim from the payload and check if it's expired
                long expSeconds = payloadJson.GetProperty("exp").GetInt64();
                DateTime expDateUtc = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;

                if (expDateUtc < DateTime.UtcNow)
                {
                    string path = new Uri(_navigationManager.Uri).LocalPath;
                    _navigationManager.NavigateTo($"/logout?returnUrl={path}", forceLoad: true);

                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
        }

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}
