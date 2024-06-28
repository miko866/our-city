using FluentValidation;
using Shared.Helpers;
using Shared.InputModels;
using Shared.InputModels.User;

namespace Shared.Validators.UserValidator;

public class UpdateUserValidator : AbstractValidator<ApplicationUserInputModel>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Gender)
            .Must(x => x!.Equals(Constants.Gender.Female) || x.Equals(Constants.Gender.Male))
            .When(s => !string.IsNullOrEmpty(s.Gender));
        ValidatorExtensions.IsNullOrNotOlderThan<ApplicationUserInputModel>(RuleFor(x => x.DateOfBirth), 120);
    }
}
