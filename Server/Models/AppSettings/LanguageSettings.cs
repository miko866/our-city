using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public class LanguageSettings
{
    [Required(ErrorMessage = "Supported Languages cannot be empty.")]
    [MinLength(1, ErrorMessage = "Supported Languages must have at least one element.")]
    public string[] SupportedLanguages { get; set; } = null!;
}
