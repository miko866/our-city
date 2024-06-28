namespace Shared.Models;

public record ModuleServiceModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string? Description { get; set; }
    public string ModuleType { get; set; } = null!;
}
