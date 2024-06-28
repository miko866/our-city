using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleNewsFileItem : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string FileItemId { get; set; } = null!;
    public FileItem FileItem { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleNewsId { get; set; } = null!;
    public ModuleNews ModuleNews { get; set; } = null!;

    public bool IsFeaturedImage { get; set; }

    public int? OrderNr { get; set; }
}
