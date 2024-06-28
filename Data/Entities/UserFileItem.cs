using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class UserFileItem : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string FileItemId { get; set; } = null!;
    public FileItem FileItem { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public bool IsAvatar { get; set; }

    public int? OrderNr { get; set; }
}
