using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Abbreviation), IsUnique = true)]
public class Country : BaseModel
{
    [Required]
    [StringLength(500)]
    [MinLength(4)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(6)]
    [MinLength(2)]
    public string Abbreviation { get; set; } = null!;

    public ICollection<State> States { get; set; } = new List<State>();
}
