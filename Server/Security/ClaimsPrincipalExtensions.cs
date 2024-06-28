using System.Security.Claims;

namespace Server.Security;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserGuid(this ClaimsPrincipal self)
    {
        var identity = self.Identity as ClaimsIdentity;
        string? guid = identity?.Claims.FirstOrDefault(x => x.Type == "sub")!.Value;

        return guid ?? "";
    }

    public static bool IsUserRole(this ClaimsPrincipal self, string roleName)
    {
        var identity = self.Identity as ClaimsIdentity;
        Claim? role = identity?.Claims.FirstOrDefault(x =>
            x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        );

        if (role is null)
            return false;
        return role.Value == roleName;
    }
}
