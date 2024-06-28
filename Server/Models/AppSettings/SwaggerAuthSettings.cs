using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record SwaggerAuthSettings
{
    [Required(ErrorMessage = "Swagger Auth Username cannot be empty.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Swagger Auth Password cannot be empty.")]
    public string Password { get; set; } = null!;
}
