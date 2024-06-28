using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

public class UserOrganisationPermission : BaseModel
{
    [Required]
    [MaxLength(8000)]
    public string UserOrganisationId { get; set; } = null!;
    public UserOrganisation ApplicationUser { get; set; } = null!;

    [Required]
    [MaxLength(8000)]
    public string PermissionId { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}
