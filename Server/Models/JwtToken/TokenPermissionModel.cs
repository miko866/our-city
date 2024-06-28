namespace Server.Models.JwtToken;

public record TokenPermissionModel
{
    public string PermissionId { get; set; } = null!;
    public string PermissionName { get; set; } = null!;
}
