namespace Server.GraphQL.Queries;

public partial class Query
{
    private readonly IHostEnvironment _environment;

    public Query(IHostEnvironment environment)
    {
        _environment = environment;
    }

    [GraphQLDescription("Get current environment")]
    public Task<string> Environment()
    {
        return Task.FromResult(_environment.EnvironmentName);
    }
}
