namespace Shared.Models;

public record MessageModel
{
    public string Message { get; set; } = null!;
    public string Category { get; set; } = null!;
}
