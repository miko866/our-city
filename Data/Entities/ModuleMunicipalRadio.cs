using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

// Udalosti
public class ModuleMunicipalRadio : BaseModel
{
    [StringLength(600)]
    [MinLength(6)]
    [Required]
    public string ShorText { get; set; } = null!;

    [MaxLength(8000)]
    [Required]
    public string OrganisationModuleServiceId { get; set; } = null!;
    public OrganisationModuleService OrganisationModuleService { get; set; } = null!;

    public List<ModuleMunicipalRadioMessage> ModuleMunicipalRadioMessages { get; } = [];
}
