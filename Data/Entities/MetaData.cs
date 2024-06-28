using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(KeyValue), IsUnique = true)]
public class MetaData : BaseModel
{
    [StringLength(256)]
    [MinLength(2)]
    [Required]
    public string KeyValue { get; set; } = null!;

    [MaxLength(8000)]
    [Required]
    public string MetaValue { get; set; } = null!;

    public List<ModuleNewsMetaData>? MetaDataModuleNews { get; } = [];
}
