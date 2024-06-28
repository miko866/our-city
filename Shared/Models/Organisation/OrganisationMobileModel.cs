namespace Shared.Models.Organisation;

public record OrganisationMobileModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Logo { get; set; }
    public string? LogoMini { get; set; }
    public ICollection<ModuleServiceContentModel> Modules { get; set; } = new List<ModuleServiceContentModel>();
}
