using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Tag : BaseModel
{
    [StringLength(256)]
    [MinLength(2)]
    [Required]
    public string Name { get; set; } = null!;

    [MaxLength(7)]
    public string? Color { get; set; }

    public List<ModuleNewsTag>? TagModuleNews { get; } = [];
    public List<ModuleEventTag>? TagModuleEvents { get; } = [];
}
