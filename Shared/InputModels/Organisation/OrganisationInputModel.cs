namespace Shared.InputModels.Organisation;

public record OrganisationInputModel
{
    public string? Name { get; set; }

    public string? Street { get; set; }

    public string? StreetNr { get; set; }

    public string? Zip { get; set; }

    public string? City { get; set; }

    public int? DistrictId { get; set; }

    public string? Description { get; set; }
}
