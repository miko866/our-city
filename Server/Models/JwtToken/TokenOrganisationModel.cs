namespace Server.Models.JwtToken;

public record TokenOrganisationModel
{
    public string OrganisationId { get; set; } = null!;
    public string OrganisationName { get; set; } = null!;
    public bool PrimaryStatus { get; set; }
    public ICollection<TokenPermissionModel>? Permissions { get; set; }
}
