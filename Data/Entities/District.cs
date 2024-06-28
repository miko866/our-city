using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

// Okres
public class District : BaseModel
{
    [Required]
    [StringLength(600)]
    [MinLength(2)]
    public string Name { get; set; } = null!;

    [StringLength(6)]
    [MinLength(2)]
    public string Abbreviation { get; set; } = null!;

    [Required]
    [StringLength(8000)]
    public string StateId { get; set; } = null!;

    [Required]
    public State State { get; set; } = null!;

    public ICollection<Organisation> Organisations { get; } = new List<Organisation>();
}
