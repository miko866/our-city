namespace Shared.Models.ModuleEvent;

public record ModuleEventMobileModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ShorText { get; set; } = null!;
    public string? Context { get; set; }
    public string? UrlLink { get; set; }
    public string? VideoLink { get; set; }
    public string? FeaturedImage { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<GalleryModel>? Gallery { get; set; } = new List<GalleryModel>();
    public ICollection<TagModel>? Tags { get; set; } = new List<TagModel>();
}
