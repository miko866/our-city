using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleNewsMetaData : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string MetaDataId { get; set; } = null!;
    public MetaData MetaData { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleNewsId { get; set; } = null!;
    public ModuleNews ModuleNews { get; set; } = null!;
}
