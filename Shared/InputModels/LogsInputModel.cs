namespace Shared.InputModels;

public record LogsInputModel
{
    public string? Application { get; set; }

    public string? Logged { get; set; }

    public string? Level { get; set; }

    public string? Message { get; set; }

    public string? Logger { get; set; }

    public string? Callsite { get; set; }

    public string? Exception { get; set; }

    public string? Guid { get; set; }
}
