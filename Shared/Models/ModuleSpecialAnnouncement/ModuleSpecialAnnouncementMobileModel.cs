namespace Shared.Models.ModuleSpecialAnnouncement;

public record ModuleSpecialAnnouncementMobileModel
{
    public string Id { get; set; } = null!;
    public string TextMessage { get; set; } = null!;
    public string Severity { get; set; } = null!;
    public string UrlLink { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
}
