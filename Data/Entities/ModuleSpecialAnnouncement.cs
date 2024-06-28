using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Data.Models.Enums;

namespace Data.Entities;

// Mimoriadne oznamy
public class ModuleSpecialAnnouncement : BaseModel
{
    [MaxLength(8000)]
    [Required]
    public string TextMessage { get; set; } = null!;

    [Required]
    public Enums.EnumSeverity Severity { get; set; }

    [MaxLength(8000)]
    public string? UrlLink { get; set; }

    [MaxLength(8000)]
    [Required]
    public string OrganisationModuleServiceId { get; set; } = null!;
    public OrganisationModuleService OrganisationModuleService { get; set; } = null!;
}
