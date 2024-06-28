using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class Message : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string TextMessage { get; set; } = null!;

    [Required]
    [StringLength(256)]
    [MinLength(2)]
    public string Category { get; set; } = null!;

    public List<ModuleMunicipalRadioMessage> ModuleMunicipalRadioMessages { get; } = [];
}
