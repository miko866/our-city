namespace Shared.InputModels.User;

/// <summary>
/// Only for Authenticate (Login)
/// </summary>
public record LoginDetailsInputModel
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = null!;
}
