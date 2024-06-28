using System.Text.Json;
using Client.GraphQL;
using Shared.Models.User;
using StrawberryShake;

namespace Client.Services.GraphQLServices;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetUsers();
}

public class UserService : IUserService
{
    private readonly GraphQLClient _graphQlClient;

    public UserService(GraphQLClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }

    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        var result = await _graphQlClient.GetUsers.ExecuteAsync();

        if (!result.IsSuccessResult())
            throw new Exception(result.Errors[0].Message);

        string jsonResponse = JsonSerializer.Serialize(result.Data?.Users);

        return JsonSerializer.Deserialize<IEnumerable<UserModel>>(jsonResponse)!;
    }
}
