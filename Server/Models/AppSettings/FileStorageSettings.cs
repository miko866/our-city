using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record FileStorageSettings
{
    [Required(ErrorMessage = "Storage Path cannot be empty.")]
    public string StoragePath { get; set; } = null!;
}
