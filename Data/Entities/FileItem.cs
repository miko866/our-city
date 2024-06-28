using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(FileName), IsUnique = true)]
[Index(nameof(Href), IsUnique = true)]
public class FileItem : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string FileName { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string FileOriginName { get; set; } = null!;

    [Required]
    [StringLength(6)]
    [MinLength(2)]
    public string FileExtension { get; set; } = null!;

    [MaxLength(8000)]
    [Required]
    public string Href { get; set; } = null!;

    [MaxLength(8000)]
    public string? AltText { get; set; }

    public FileItemType FileItemType { get; set; } = null!;

    [MaxLength(8000)]
    public string FileItemTypeId { get; set; } = null!;

    public List<UserFileItem> UserFileItems { get; } = [];
    public List<OrganisationFileItem> OrganisationFileItems { get; } = [];
    public List<ModuleNewsFileItem> ModuleNewsFileItems { get; } = [];
    public List<ModuleEventFileItem> ModuleEventFileItems { get; } = [];
    public List<ModuleSimplePageFileItem> ModuleSimplePageFileItems { get; } = [];
}
