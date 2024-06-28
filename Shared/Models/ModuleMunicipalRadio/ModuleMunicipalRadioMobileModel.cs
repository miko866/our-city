namespace Shared.Models.ModuleMunicipalRadio;

public record ModuleMunicipalRadioMobileModel
{
    public string Id { get; set; } = null!;
    public string ShorText { get; set; } = null!;
    public ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
}
