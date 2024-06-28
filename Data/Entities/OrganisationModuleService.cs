using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class OrganisationModuleService : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string ModuleServiceId { get; set; } = null!;
    public ModuleService ModuleService { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string OrganisationId { get; set; } = null!;
    public Organisation Organisation { get; set; } = null!;

    public bool IsActive { get; set; }

    public ICollection<ModuleNews> ModuleNews { get; } = new List<ModuleNews>();
    public ICollection<ModuleEvent> ModuleEvents { get; } = new List<ModuleEvent>();
    public ICollection<ModuleMunicipalRadio> ModuleMunicipalRadios { get; } = new List<ModuleMunicipalRadio>();
    public ICollection<ModuleSpecialAnnouncement> ModuleSpecialAnnouncements { get; } =
        new List<ModuleSpecialAnnouncement>();
    public ICollection<ModuleSimplePage> ModuleSimplePages { get; } = new List<ModuleSimplePage>();
}
