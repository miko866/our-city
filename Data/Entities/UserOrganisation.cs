using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class UserOrganisation : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string OrganisationId { get; set; } = null!;
    public Organisation Organisation { get; set; } = null!;

    public bool IsPrimary { get; set; }

    public List<UserOrganisationPermission> UserOrganisationPermissions { get; } = [];
}
