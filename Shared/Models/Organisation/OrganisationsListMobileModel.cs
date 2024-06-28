namespace Shared.Models.Organisation;

public record OrganisationsListMobileModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Logo { get; set; }
    public string? LogoMini { get; set; }
}
