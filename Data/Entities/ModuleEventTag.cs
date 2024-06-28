using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleEventTag : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string TagId { get; set; } = null!;
    public Tag Tag { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleEventId { get; set; } = null!;
    public ModuleEvent ModuleEvent { get; set; } = null!;
}
