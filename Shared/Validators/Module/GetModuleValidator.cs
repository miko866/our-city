using FluentValidation;
using Shared.InputModels;

namespace Shared.Validators.NewsModuleValidator;

public class GetModuleValidator : AbstractValidator<ModuleGetInputModel>
{
    public GetModuleValidator()
    {
        RuleFor(x => x.OrganisationId).NotEmpty();
        RuleFor(x => x.ModuleServiceId).NotEmpty();
    }
}
