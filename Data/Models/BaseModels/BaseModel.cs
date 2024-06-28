using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.BaseModels;

// General data for Entities
public class BaseModel
{
    protected BaseModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [MaxLength(8000)]
    public string Id { get; set; } // ID is not nullable!

    [Required]
    [MaxLength(256)]
    public string CreatedBy { get; set; } = null!;

    [MaxLength(256)]
    public string? UpdatedBy { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
