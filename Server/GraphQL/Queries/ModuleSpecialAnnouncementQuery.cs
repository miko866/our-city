using HotChocolate.Authorization;
using Server.Services;
using Shared.InputModels;
using Shared.Models.ModuleSpecialAnnouncement;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize]
    [GraphQLDescription("Get special announcement for a selected organisation, mobile view")]
    public async Task<IEnumerable<ModuleSpecialAnnouncementMobileModel>> ModuleSpecialAnnouncementMobile(
        [Service] IModuleSpecialAnnouncementService service,
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleSpecialAnnouncementMobile(data, cancellationToken);
    }
}
