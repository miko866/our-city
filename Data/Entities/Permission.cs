using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Permission : BaseModel
{
    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = null!;

    public List<UserOrganisationPermission> UserOrganisationPermissions { get; } = [];
}
