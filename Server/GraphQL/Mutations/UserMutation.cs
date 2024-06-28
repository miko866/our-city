using System.Security.Claims;
using Data.Entities;
using HotChocolate.Authorization;
using Server.Helpers;
using Server.Security;
using Server.Services;
using Shared.InputModels;
using Shared.InputModels.User;
using Constants = Shared.Helpers.Constants;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.GraphQL.Mutations;

public partial class Mutation
{
    private readonly IErrorMessages _errorMessages;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="errorMessages"></param>
    public Mutation(IErrorMessages errorMessages)
    {
        _errorMessages = errorMessages;
    }

    [GraphQLDescription(
        "Only admins can create a new user. This will create an User without Email Confirmation from Admin Page"
    )]
    [Authorize(Roles = [Constants.Roles.Admin])]
    public async Task<ApplicationUser> CreateUser(
        [Service] IUserService userService,
        ApplicationUserInputModel applicationUserInput,
        CancellationToken cancellationToken
    )
    {
        ApplicationUser result = await userService.CreateUser(applicationUserInput, cancellationToken);
        return result;
    }

    [GraphQLDescription("Update a user by filter")]
    [Authorize]
    public async Task<ApplicationUser> UpdateUser(
        [Service] IUserService userService,
        ApplicationUserInputModel applicationUserInput,
        string applicationUserId,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        if (!PermissionChecker.CanQueryOrMutate(applicationUserId, claimsPrincipal))
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_NOT_ALLOWED())
                    .SetCode(ErrorCodes.CODE_ERROR_NOT_ALLOWED)
                    .Build()
            );

        bool isAdmin = claimsPrincipal.IsUserRole(Shared.Helpers.Constants.Roles.Admin);
        ApplicationUser result = await userService.UpdateUser(
            applicationUserInput,
            applicationUserId,
            isAdmin,
            cancellationToken
        );
        return result;
    }

    [GraphQLDescription("Soft delete or renew for any user")]
    [Authorize]
    public async Task<bool> SoftDeleteUser(
        [Service] IUserService userService,
        string applicationUserId,
        bool isDeleted,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        if (!PermissionChecker.CanQueryOrMutate(applicationUserId, claimsPrincipal))
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_NOT_ALLOWED())
                    .SetCode(ErrorCodes.CODE_ERROR_NOT_ALLOWED)
                    .Build()
            );

        bool result = await userService.SoftDeleteUser(applicationUserId, isDeleted, cancellationToken);
        return result;
    }
}
