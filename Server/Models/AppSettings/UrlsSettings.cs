using System.ComponentModel.DataAnnotations;

namespace Server.Models.AppSettings;

public record UrlsSettings
{
    [Required(ErrorMessage = "CORS Client cannot be empty."), Url(ErrorMessage = "CORS Client need to be URL.")]
    public string Client { get; set; } = null!;

    [Required(ErrorMessage = "GIS Search API cannot be empty."), Url(ErrorMessage = "GIS Search API need to be URL.")]
    public string GisSearch { get; set; } = null!;
}
