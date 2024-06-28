using System.ComponentModel.DataAnnotations;

namespace Shared.Models.User;

public record LoginDetailsModel
{
    public string? UserName { get; set; }
    public string? Email { get; set; }

    [Required]
    public string Password { get; set; } = null!;
}
