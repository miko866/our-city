using Microsoft.Extensions.DependencyInjection;
using Server.Services;
using Shared.Extensions;
using Shared.InputModels.User;
using TestServer.Orderers;

namespace TestServer.Tests;

[Collection("Sequential")]
[TestCaseOrderer("TestServer.Orderers.PriorityOrderer", "TestServer")]
public class TestUserService : IClassFixture<OurCityWebApplicationFactory<Program>>
{
    private readonly OurCityWebApplicationFactory<Program> _factory;

    public TestUserService(OurCityWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public async Task TestAuthenticateSysAdmin()
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        // Act
        var cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        var inputs = new LoginDetailsInputModel { UserName = "SysAdminUser", Password = "Substring" };
        string results = await userService.AuthenticateUser(inputs, cancellationToken);

        // Assert
        Assert.NotNull(results);
        Assert.True(results.IsNotNull(), "Token cannot by null or empty. Wrong credentials provided.");
    }
}
