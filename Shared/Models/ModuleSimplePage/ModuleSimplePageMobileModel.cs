namespace Shared.Models.ModuleSimplePage;

public record ModuleSimplePageMobileModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Context { get; set; }
    public string? Icon { get; set; }
    public string? UrlLink { get; set; }
    public string? VideoLink { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? FeaturedImage { get; set; }
    public ICollection<GalleryModel>? Gallery { get; set; } = new List<GalleryModel>();
}
