namespace Shared.InputModels.ModuleEvent;

public record ModuleEventFilterInputModel : ModuleGetInputModel
{
    public DateTime? DateValue { get; set; }
    public List<string>? TagValues { get; set; } = [];
}
