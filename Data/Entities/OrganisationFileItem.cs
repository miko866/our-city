using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class OrganisationFileItem : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string FileItemId { get; set; } = null!;
    public FileItem FileItem { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string OrganisationId { get; set; } = null!;
    public Organisation Organisation { get; set; } = null!;

    public bool IsLogo { get; set; }
    public bool IsLogoMini { get; set; }

    public int? OrderNr { get; set; }
}
