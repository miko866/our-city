namespace Shared.Models.User;

public record UserModel
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string Gender { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }

    public ICollection<string> Roles { get; set; } = [];
    public ICollection<string> CurrentPermissions { get; set; } = [];

    public string? CurrentOrganisationId { get; set; }

    public string VisibleName => $"{FirstName} {LastName}";
}
