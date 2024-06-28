using Server.Services;

namespace Server.GraphQL.Queries;

public partial class Query
{
    [GraphQLDescription("Create default users and seed DB. Only for Local/Developing and Testing.")]
    public async Task<string> CreateDefaultUsersAndSeed(
        [Service] ISeederService sederService,
        CancellationToken cancellationToken
    )
    {
        return await sederService.SeedDb(cancellationToken);
    }
}
