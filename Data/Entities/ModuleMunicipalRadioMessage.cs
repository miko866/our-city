using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class ModuleMunicipalRadioMessage : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string MessageId { get; set; } = null!;
    public Message Message { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string ModuleMunicipalRadioId { get; set; } = null!;
    public ModuleMunicipalRadio ModuleMunicipalRadio { get; set; } = null!;
}
