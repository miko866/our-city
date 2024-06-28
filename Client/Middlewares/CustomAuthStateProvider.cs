using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Client.Helpers;
using Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Middlewares;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly AuthTokenService _authTokenService;

    public CustomAuthStateProvider(
        ILocalStorageService localStorage,
        HttpClient http,
        AuthTokenService authTokenService
    )
    {
        _localStorage = localStorage;
        _http = http;
        _authTokenService = authTokenService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _authTokenService.SetAuthToken(await _localStorage.GetItemAsStringAsync("token") ?? string.Empty);

        var identity = new ClaimsIdentity();
        _http.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(_authTokenService.GetAuthToken()))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(_authTokenService.GetAuthToken()), "jwt");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                _authTokenService.GetAuthToken().Replace("\"", "")
            );
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    protected virtual IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        const string roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken? DecodedJWT = handler.ReadJwtToken(jwt);
        string payloadNEW = DecodedJWT.EncodedPayload;

        byte[] jsonBytes = JwtHelper.FromBase64Url(payloadNEW);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        List<Claim> claim = keyValuePairs!
            .Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty))
            .ToList();

        if (!JwtHelper.CheckStringList(keyValuePairs, roleClaimType))
            return claim;

        AddRolesIntoClaim(keyValuePairs!, roleClaimType, claim);

        return claim;
    }

    private static void AddRolesIntoClaim(
        Dictionary<string, object> keyValuePairs,
        string roleClaimType,
        List<Claim> claim
    )
    {
        string[] roles = keyValuePairs[roleClaimType].ToString()!.Split(",");
        keyValuePairs.Remove(roleClaimType);
        claim.Add(new Claim(roleClaimType, roles[0].Replace("[", "").Replace("\"", "")));
        claim.Add(new Claim(roleClaimType, roles[1].Replace("]", "").Replace("\"", "")));
    }
}
