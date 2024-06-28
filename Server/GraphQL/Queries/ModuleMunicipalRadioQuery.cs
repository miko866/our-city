using HotChocolate.Authorization;
using Server.Services;
using Shared.InputModels;
using Shared.Models.ModuleMunicipalRadio;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize]
    [GraphQLDescription("Get radios messages for a selected organisation, mobile view")]
    public async Task<IEnumerable<ModuleMunicipalRadioMobileModel>> ModuleMunicipalRadioMobile(
        [Service] IModuleMunicipalRadioService service,
        ModuleGetInputModel data,
        CancellationToken cancellationToken
    )
    {
        return await service.GetModuleMunicipalRadioMobile(data, cancellationToken);
    }
}
