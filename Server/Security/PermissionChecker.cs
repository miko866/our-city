using System.Security.Claims;
using Server.Helpers;

namespace Server.Security;

public static class PermissionChecker
{
    /// <summary>
    /// Determines whether the specified user has permission to query or mutate data based on their user ID and administrative status.
    /// If no ID was provided in the where statement return true
    /// </summary>
    /// <param name="applicationUserId"></param>
    /// <param name="claimsPrincipal">The user's claims principal.</param>
    /// <returns><c>true</c> if the user has permission to query or mutate data; otherwise, <c>false</c>.</returns>
    public static bool CanQueryOrMutate(string? applicationUserId, ClaimsPrincipal claimsPrincipal)
    {
        if (string.IsNullOrEmpty(applicationUserId))
            return true;

        bool isAdmin = claimsPrincipal.IsUserRole(Shared.Helpers.Constants.Roles.Admin);
        string userId = claimsPrincipal.GetUserGuid();

        return userId == applicationUserId || isAdmin;
    }
}
