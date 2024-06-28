using System.Text.Json;
using Client.GraphQL;
using Shared.InputModels;
using StrawberryShake;

namespace Client.Services.GraphQLServices;

public interface IAuthService
{
    Task<string> AuthenticateUser(LoginDetailsInputModelInput loginDetailsInput);
}

public class AuthService : IAuthService
{
    private readonly GraphQLClient _graphQlClient;

    public AuthService(GraphQLClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }

    public async Task<string> AuthenticateUser(LoginDetailsInputModelInput loginDetailsInput)
    {
        var result = await _graphQlClient.AuthenticateUser.ExecuteAsync(loginDetailsInput);

        if (!result.IsSuccessResult())
            throw new Exception(result.Errors[0].Message);

        string jsonResponse = JsonSerializer.Serialize(result.Data?.AuthenticateUser);

        return JsonSerializer.Deserialize<string>(jsonResponse)!;
    }
}
