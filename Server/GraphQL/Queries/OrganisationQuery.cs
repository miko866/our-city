using Data.Entities;
using HotChocolate.Authorization;
using Server.Services;
using Shared.Helpers;
using Shared.Models.Organisation;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize(Roles = [Constants.Roles.Admin])]
    [GraphQLDescription("Get all organisations for admin view")]
    public async Task<IEnumerable<Organisation>> Organisations(
        [Service] IOrganisationService service,
        CancellationToken cancellationToken
    )
    {
        return await service.GetOrganisations(cancellationToken);
    }

    [Authorize]
    [GraphQLDescription("Get all organisations as simple response for mobile app")]
    public async Task<IEnumerable<OrganisationsListMobileModel>> OrganisationsListMobile(
        [Service] IOrganisationService service,
        CancellationToken cancellationToken
    )
    {
        return await service.GetOrganisationsListMobile(cancellationToken);
    }

    [Authorize]
    [GraphQLDescription("Get organisation with modules by ID for mobile app")]
    public async Task<OrganisationMobileModel> OrganisationByIdMobile(
        [Service] IOrganisationService service,
        string id,
        CancellationToken cancellationToken
    )
    {
        return await service.GetOrganisationByIdMobile(id, cancellationToken);
    }
}
