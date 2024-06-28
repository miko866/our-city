using FluentValidation;
using Shared.Extensions;
using Shared.InputModels.ModuleEvent;

namespace Shared.Validators.ModuleEvent;

public class ModuleEventFilterInputValidator : AbstractValidator<ModuleEventFilterInputModel>
{
    public ModuleEventFilterInputValidator()
    {
        RuleFor(x => x.OrganisationId).NotEmpty();
        RuleFor(x => x.ModuleServiceId).NotEmpty();
        RuleFor(x => x.DateValue).Empty().When(x => !x.TagValues.IsNotNull());
        RuleFor(x => x.TagValues).Empty().When(x => !string.IsNullOrEmpty(x.DateValue.ToString()));
    }
}
