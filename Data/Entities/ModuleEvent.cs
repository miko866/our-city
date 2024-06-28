using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

// Udalosti
public class ModuleEvent : BaseModel
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

    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    [MaxLength(8000)]
    [Required]
    public string OrganisationModuleServiceId { get; set; } = null!;
    public OrganisationModuleService OrganisationModuleService { get; set; } = null!;

    public List<ModuleEventTag>? TagModuleEvents { get; } = [];
    public List<ModuleEventFileItem> ModuleEventFileItems { get; } = [];
}
