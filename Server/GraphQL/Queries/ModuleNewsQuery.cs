using HotChocolate.Authorization;
using Server.Services;
using Shared.InputModels;
using Shared.Models.ModuleNews;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize]
    [GraphQLDescription("Get news for a selected organisation, mobile view")]
    public async Task<IEnumerable<ModuleNewsMobileModel>> ModuleNewsMobile(
        [Service] IModuleNewsService service,
        ModuleGetInputModel moduleGetInput,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleNewsMobile(moduleGetInput, cancellationToken);
    }

    [Authorize]
    [GraphQLDescription("Get news by id for a selected organisation, mobile view")]
    public async Task<ModuleNewsMobileModel> ModuleNewsByIdMobile(
        [Service] IModuleNewsService service,
        string id,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleNewsByIdMobile(id, cancellationToken);
    }
}
