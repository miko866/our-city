using FluentValidation;
using Shared.InputModels;
using Shared.InputModels.Organisation;

namespace Shared.Validators.OrganisationValidator;

public class CreateOrganisationValidator : AbstractValidator<OrganisationInputModel>
{
    public CreateOrganisationValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.Street).NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.StreetNr).NotEmpty().MinimumLength(1).MaximumLength(255);
        RuleFor(x => x.Zip).NotEmpty().MinimumLength(4).MaximumLength(12);
        RuleFor(x => x.City).NotEmpty().MinimumLength(4).MaximumLength(12);
        RuleFor(x => x.DistrictId).NotEmpty();
        RuleFor(x => x.Description).MinimumLength(4).MaximumLength(8000);
    }
}
