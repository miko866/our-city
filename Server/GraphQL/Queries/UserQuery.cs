using System.Security.Claims;
using Data.Entities;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Server.Services;
using Shared.Helpers;
using Shared.InputModels;
using Shared.InputModels.User;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [GraphQLDescription("Authenticate user - login and get token")]
    public async Task<string> AuthenticateUser(
        [Service] IUserService service,
        LoginDetailsInputModel loginDetailsInput,
        CancellationToken cancellationToken
    )
    {
        return await service.AuthenticateUser(loginDetailsInput, cancellationToken);
    }

    [Authorize]
    [GraphQLDescription("Get current logged user")]
    public async Task<ApplicationUser> CurrentUser(
        [Service] IUserService service,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken
    )
    {
        return await service.GetCurrentUser(claimsPrincipal, cancellationToken);
    }

    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Get all or some users by filter for admin view")]
    [Authorize(Roles = [Constants.Roles.Admin])]
    public async Task<IEnumerable<ApplicationUser>> Users(
        [Service] IUserService service,
        IResolverContext resolverContext,
        CancellationToken cancellationToken
    )
    {
        return await service.GetUsers(resolverContext, cancellationToken);
    }

    // TODO: Get User By Organisation
}
