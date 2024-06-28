using Microsoft.Extensions.DependencyInjection;
using Server.Services;
using Shared.Extensions;
using TestServer.Orderers;

namespace TestServer.Tests;

[Collection("Sequential")]
[TestCaseOrderer("TestServer.Orderers.PriorityOrderer", "TestServer")]
public class TestMobileAppService : IClassFixture<OurCityWebApplicationFactory<Program>>
{
    private readonly OurCityWebApplicationFactory<Program> _factory;

    public TestMobileAppService(OurCityWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public async Task TestCreateMobileAppToken()
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IMobileAppService>();

        // Act
        string result = await service.CreateMobileAppToken();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsNotNull(), "Expected to be not empty or null.");
    }
}
