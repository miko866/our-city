using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class ModuleSimplePage : BaseModel
{
    [StringLength(256)]
    [MinLength(6)]
    [Required]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    [MinLength(6)]
    [Required]
    public string Title { get; set; } = null!;

    [MaxLength(8000)]
    public string? Context { get; set; }

    [StringLength(256)]
    [MinLength(10)]
    public string? Icon { get; set; }

    [MaxLength(8000)]
    public string? UrlLink { get; set; }

    [MaxLength(8000)]
    public string? VideoLink { get; set; }

    [MaxLength(8000)]
    [Required]
    public string OrganisationModuleServiceId { get; set; } = null!;
    public OrganisationModuleService OrganisationModuleService { get; set; } = null!;

    public List<ModuleSimplePageFileItem> ModuleSimplePageFileItems { get; } = [];
}
