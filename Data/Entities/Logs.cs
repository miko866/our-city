using System.ComponentModel.DataAnnotations;
using Data.Models.BaseModels;

namespace Data.Entities;

//! This Entity creates the Nlog "logs" table, that why it is in plural.
public class Logs : BaseModel
{
    [MaxLength(8000)]
    public string Application { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Logged { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Level { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Message { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Logger { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Callsite { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Exception { get; set; } = string.Empty;

    [MaxLength(8000)]
    public string Guid { get; set; } = string.Empty;
}
