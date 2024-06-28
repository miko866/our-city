using HotChocolate.Authorization;
using Server.Services;
using Shared.InputModels.ModuleEvent;
using Shared.Models.ModuleEvent;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize]
    [GraphQLDescription("Get events for a selected organisation, mobile view")]
    public async Task<IEnumerable<ModuleEventMobileModel>> ModuleEventMobile(
        [Service] IModuleEventService service,
        ModuleEventFilterInputModel data,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleEventMobile(data, cancellationToken);
    }

    [Authorize]
    [GraphQLDescription("Get event by id for a selected organisation, mobile view")]
    public async Task<ModuleEventMobileModel> ModuleEventMobileByIdMobile(
        [Service] IModuleEventService service,
        string id,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleEventMobileByIdMobile(id, cancellationToken);
    }
}
