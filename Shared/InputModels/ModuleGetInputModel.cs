namespace Shared.InputModels;

public record ModuleGetInputModel
{
    public string OrganisationId { get; set; } = null!;
    public string ModuleServiceId { get; set; } = null!;
}
