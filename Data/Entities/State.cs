using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models.BaseModels;

namespace Data.Entities;

// That means (Kraj, Kanton ...)
public class State : BaseModel
{
    [Required]
    [StringLength(600)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(6)]
    [MinLength(2)]
    public string Abbreviation { get; set; } = null!;

    [Required]
    [StringLength(8000)]
    public string CountryId { get; set; } = null!;

    [Required]
    public Country Country { get; set; } = null!;

    public ICollection<District> Districts { get; } = new List<District>();
}
