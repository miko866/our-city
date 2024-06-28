using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Server.Models.JwtToken;
using Shared.Extensions;

namespace Server.Security;

#region Interface
public interface IJwtGenerator
{
    Task<string> GetJwtSecurityToken(ApplicationUser user, TokenExtensionModel? tokenExtension = null);
    Task<string> GenerateMobileAppJwtToken();
}
#endregion Interface

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="userManager"></param>
    public JwtGenerator(IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    /// <summary>
    /// Create JWT Token for logged user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="permissions"></param>
    /// <param name="organisationId"></param>
    /// <returns></returns>
    public async Task<string> GetJwtSecurityToken(ApplicationUser user, TokenExtensionModel? tokenExtension = null)
    {
        byte[] keyInBytes = System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("JwtOptions:SecretKey").Value!
        );

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(keyInBytes),
            SecurityAlgorithms.HmacSha512Signature
        );
        DateTime jwtDate = DateTime.Now;
        var options = new IdentityOptions();

        // Add new claims
        var tokenClaims = new List<Claim>
        {
            // https://stackoverflow.com/questions/28907831/how-to-use-jti-claim-in-a-jwt
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(jwtDate).ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName + ' ' + user.LastName),
            new(options.ClaimsIdentity.UserNameClaimType, user.UserName!),
            // new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id) // OPTIONAL
            // new Claim("custom_name", user.Id.toString()) // EXAMPLE custom Claim name with value
        };

        if (tokenExtension.IsNotNull())
            tokenClaims.Add(
                new Claim("permissions", JsonSerializer.Serialize(tokenExtension), JsonClaimValueTypes.JsonArray)
            );

        // Adds roles into jwt token claim
        IList<string> roles = await _userManager.GetRolesAsync(user);
        tokenClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Make JWT token
        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JwtOptions:Issuer").Value,
            audience: _configuration.GetSection("JwtOptions:Audience").Value,
            // Token Payload
            claims: tokenClaims,
            notBefore: jwtDate,
            // Should be short-lived. For logins, it may be fine to use 24h
            expires: jwtDate.AddHours(24),
            // Provide a cryptographic key used to sign the token.
            // When dealing with symmetric keys then this must be the same key used to validate the token.
            signingCredentials: credentials
        );

        // Set current user details for business & common library
        ApplicationUser currentUser = (await _userManager.FindByEmailAsync(user.Email!))!;

        // Add new claim details
        IList<Claim> existingClaims = await _userManager.GetClaimsAsync(currentUser);
        await _userManager.RemoveClaimsAsync(currentUser, existingClaims);
        await _userManager.AddClaimsAsync(currentUser, tokenClaims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Task<string> GenerateMobileAppJwtToken()
    {
        byte[] keyInBytes = System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("MobileAppToken:SecretKey").Value!
        );
        string subValue = _configuration.GetSection("MobileAppToken:Sub").Value!;

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(keyInBytes),
            SecurityAlgorithms.HmacSha512Signature
        );
        DateTime jwtDate = DateTime.Now;

        // Add new claims
        var tokenClaims = new List<Claim>
        {
            // https://stackoverflow.com/questions/28907831/how-to-use-jti-claim-in-a-jwt
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, subValue),
            new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(jwtDate).ToString(), ClaimValueTypes.Integer64)
        };

        // Make JWT token
        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("MobileAppToken:Issuer").Value,
            audience: _configuration.GetSection("MobileAppToken:Audience").Value,
            // Token Payload
            claims: tokenClaims,
            notBefore: jwtDate,
            expires: jwtDate.AddMonths(3),
            // Provide a cryptographic key used to sign the token.
            // When dealing with symmetric keys then this must be the same key used to validate the token.
            signingCredentials: credentials
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
