using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record FileSettings
{
    [Required(ErrorMessage = "Max File Upload Size In MB cannot be empty.")]
    public string MaxFileUploadSizeInMB { get; set; } = null!;
}
