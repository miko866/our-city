using HotChocolate.Authorization;
using Server.Services;
using Shared.InputModels;
using Shared.Models.ModuleSimplePage;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize]
    [GraphQLDescription("Get simple page for a selected organisation, mobile view")]
    public async Task<IEnumerable<ModuleSimplePageMobileModel>> ModuleSimplePageMobile(
        [Service] IModuleSimplePageService service,
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleSimplePageMobile(data, cancellationToken);
    }

    [Authorize]
    [GraphQLDescription("Get simple page by id for a selected organisation, mobile view")]
    public async Task<ModuleSimplePageMobileModel> ModuleSimplePageMobileByIdMobile(
        [Service] IModuleSimplePageService service,
        string id,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleSimplePageMobileByIdMobile(id, cancellationToken);
    }
}
