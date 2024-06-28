using Microsoft.AspNetCore.Authorization;
using Server.Services;
using Shared.Helpers;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [Authorize(Roles = Constants.Roles.Admin)]
    [GraphQLDescription("Create Mobile auth token for backend mobile endpoints")]
    public async Task<string> CreateMobileAppToken([Service] IMobileAppService service)
    {
        return await service.CreateMobileAppToken();
    }
}
