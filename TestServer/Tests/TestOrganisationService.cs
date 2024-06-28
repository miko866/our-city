using Microsoft.Extensions.DependencyInjection;
using Server.Services;
using Shared.Extensions;
using Shared.Models.Organisation;
using TestServer.Orderers;

namespace TestServer.Tests;

[Collection("Sequential")]
[TestCaseOrderer("TestServer.Orderers.PriorityOrderer", "TestServer")]
public class TestOrganisationService : IClassFixture<OurCityWebApplicationFactory<Program>>
{
    private readonly OurCityWebApplicationFactory<Program> _factory;

    public TestOrganisationService(OurCityWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public async Task TestGetOrganisationsListMobile()
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        var organisationService = scope.ServiceProvider.GetRequiredService<IOrganisationService>();

        // Act
        var cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        IEnumerable<OrganisationsListMobileModel> results = await organisationService.GetOrganisationsListMobile(
            cancellationToken
        );

        // Assert
        Assert.NotNull(results);
        Assert.True(results.ToList().Count > 0, "Expected to be greater than 0.");
    }

    [Fact, TestPriority(2)]
    public async Task TestGetOrganisationByIdMobile()
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        var organisationService = scope.ServiceProvider.GetRequiredService<IOrganisationService>();

        // Act
        var cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        OrganisationMobileModel result = await organisationService.GetOrganisationByIdMobile(
            "9c6c68fb-ad9f-4595-8bcf-47b99e5672ef",
            cancellationToken
        );

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsNotNull(), "Expected Novoť organisation.");
    }
}
