using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

// Means village/town/city or organization
[Index(nameof(Name), IsUnique = true)]
public class Organisation : BaseModel
{
    [StringLength(1000)]
    [MinLength(2)]
    [Required]
    public string Name { get; set; } = null!;

    [StringLength(12)]
    [MinLength(4)]
    [Required]
    public string Zip { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string DistrictId { get; set; } = null!;

    [Required]
    public District District { get; set; } = null!;

    [StringLength(8000)]
    [MinLength(10)]
    public string? Description { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [MaxLength(7)]
    public string? Color { get; set; }

    public List<UserOrganisation> UserOrganisations { get; } = [];
    public List<OrganisationFileItem> OrganisationFileItems { get; } = [];
    public List<OrganisationModuleService> OrganisationModuleServices { get; } = [];
}
