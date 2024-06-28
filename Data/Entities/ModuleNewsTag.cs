using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleNewsTag : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string TagId { get; set; } = null!;
    public Tag Tag { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleNewsId { get; set; } = null!;
    public ModuleNews ModuleNews { get; set; } = null!;
}
