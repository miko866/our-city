using FluentValidation;
using Shared.InputModels;
using Shared.InputModels.User;
using Constants = Shared.Helpers.Constants;

namespace Shared.Validators.UserValidator;

public class CreateUserValidator : AbstractValidator<ApplicationUserInputModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.UserName).MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(x =>
                x!.Equals(Constants.Gender.Female, StringComparison.OrdinalIgnoreCase)
                || x.Equals(Constants.Gender.Male, StringComparison.OrdinalIgnoreCase)
            )
            .WithMessage("Gender Value not in range!");
        RuleFor(x => x.Password).NotEmpty();
        ValidatorExtensions.IsNullOrNotOlderThan<ApplicationUserInputModel>(RuleFor(x => x.DateOfBirth), 120);
    }
}
