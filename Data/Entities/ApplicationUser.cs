using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(500)]
    [MinLength(2)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(500)]
    [MinLength(2)]
    public string? LastName { get; set; }

    public bool IsDeleted { get; set; }

    [Required]
    [StringLength(60)]
    [MinLength(2)]
    public string Gender { get; set; } = null!;

    [StringLength(8000)]
    [MinLength(2)]
    public string? Description { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [Required]
    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;

    [StringLength(256)]
    public string? UpdatedBy { get; set; }

    public List<UserOrganisation>? UserOrganisations { get; } = [];
    public List<UserFileItem>? UserFileItems { get; } = [];

    [NotMapped]
    public ICollection<string> Roles { get; set; } = [];

    [NotMapped]
    public ICollection<string> CurrentPermissions { get; set; } = [];

    [NotMapped]
    public string? CurrentOrganisationId { get; set; }

    [NotMapped]
    public string VisibleName => $"{FirstName} {LastName}";
}

//https://stackoverflow.com/questions/24364152/not-set-discriminator-column-when-save-entity-in-entity-framework
//inherit twice from the same Class to get rid of this discriminator error.
//code below does not to work in builder, therefore i do it in this way.
//modelBuilder.Entity<ApplicationUser>().Map(p => p.Requires("Discriminator").HasValue("ApplicationUser"));
public class ApplicationUserIAmNotForUsage : IdentityUser { }
