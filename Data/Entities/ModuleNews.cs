using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

// Aktuality
public class ModuleNews : BaseModel
{
    [StringLength(256)]
    [MinLength(6)]
    [Required]
    public string Title { get; set; } = null!;

    [StringLength(600)]
    [MinLength(6)]
    [Required]
    public string ShorText { get; set; } = null!;

    [MaxLength(8000)]
    public string? Context { get; set; }

    [MaxLength(8000)]
    public string? UrlLink { get; set; }

    [MaxLength(8000)]
    public string? VideoLink { get; set; }

    [MaxLength(8000)]
    [Required]
    public string OrganisationModuleServiceId { get; set; } = null!;
    public OrganisationModuleService OrganisationModuleService { get; set; } = null!;

    public List<ModuleNewsTag>? TagModuleNews { get; } = [];
    public List<ModuleNewsMetaData>? MetaDataModuleNews { get; } = [];
    public List<ModuleNewsFileItem> ModuleNewsFileItems { get; } = [];
}
