using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(ModuleType), IsUnique = true)]
public class ModuleService : BaseModel
{
    [Required]
    [StringLength(600)]
    [MinLength(10)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [MinLength(10)]
    public string Icon { get; set; } = null!;

    [StringLength(8000)]
    [MinLength(10)]
    public string? Description { get; set; }

    [Required]
    [StringLength(256)]
    [MinLength(10)]
    public string ModuleType { get; set; } = null!;

    public List<OrganisationModuleService> OrganisationModuleServices { get; } = [];
}
