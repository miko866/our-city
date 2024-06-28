using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleEventFileItem : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string FileItemId { get; set; } = null!;
    public FileItem FileItem { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleEventId { get; set; } = null!;
    public ModuleEvent ModuleEvent { get; set; } = null!;

    public bool IsFeaturedImage { get; set; }

    public int? OrderNr { get; set; }
}
