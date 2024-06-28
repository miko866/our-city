using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record EmailConfigurationSettings
{
    [Required(ErrorMessage = "Email From cannot be empty.")]
    [EmailAddress]
    public string From { get; set; } = null!;

    [Required(ErrorMessage = "Email SmtpServer cannot be empty.")]
    public string SmtpServer { get; set; } = null!;

    [Required(ErrorMessage = "Email Port cannot be empty.")]
    public int Port { get; set; }

    [Required(ErrorMessage = "Email Username cannot be empty.")]
    [EmailAddress]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email Password cannot be empty.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Email IdentityTokenKey cannot be empty.")]
    public string IdentityTokenKey { get; set; } = null!;
}
