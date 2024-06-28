using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record ConnectionStringsSettings
{
    [Required(ErrorMessage = "Connection String cannot be empty.")]
    public string Database { get; set; } = null!;
}
