using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class FileItemType : BaseModel
{
    [Required]
    public Enums.EnumFileType Name { get; set; }

    public ICollection<FileItem> FileItems { get; set; } = new List<FileItem>();
}
