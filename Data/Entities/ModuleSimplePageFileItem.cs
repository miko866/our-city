using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleSimplePageFileItem : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string FileItemId { get; set; } = null!;
    public FileItem FileItem { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleSimplePageId { get; set; } = null!;
    public ModuleSimplePage ModuleSimplePage { get; set; } = null!;

    public bool IsFeaturedImage { get; set; }

    public int? OrderNr { get; set; }
}
