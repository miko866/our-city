using Data.Entities;
using HotChocolate.Authorization;
using HotChocolate.Resolvers;
using Server.Services;
using Shared.Helpers;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize(Roles = [Constants.Roles.Admin])]
    [GraphQLDescription("Get all roles for admin view")]
    public async Task<IEnumerable<ApplicationRole>> Roles(
        [Service] IRoleService service,
        CancellationToken cancellationToken
    )
    {
        return await service.GetRoles(cancellationToken);
    }

    [Authorize(Roles = [Constants.Roles.Admin])]
    [UseFiltering]
    [GraphQLDescription("Get all or some roles by filter for admin view")]
    public async Task<ApplicationRole> Role(
        [Service] IRoleService service,
        IResolverContext resolverContext,
        CancellationToken cancellationToken
    )
    {
        return await service.GetRole(resolverContext, cancellationToken);
    }
}
