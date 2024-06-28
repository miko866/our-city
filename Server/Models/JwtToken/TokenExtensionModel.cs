namespace Server.Models.JwtToken;

public record TokenExtensionModel
{
    public ICollection<TokenOrganisationModel> Organisations { get; set; } = null!;
}
