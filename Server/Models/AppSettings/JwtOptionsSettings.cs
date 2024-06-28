using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record JwtOptionsSettings
{
    [
        Required(ErrorMessage = "JwtOptions Audience cannot be empty."),
        Url(ErrorMessage = "JwtOptions Audience need to be URL.")
    ]
    public string Audience { get; set; } = null!;

    [Required(ErrorMessage = "JwtOptions Issuer cannot be empty.")]
    public string Issuer { get; set; } = null!;

    [Required(ErrorMessage = "JwtOptions SecretKey cannot be empty.")]
    public string SecretKey { get; set; } = null!;
}
