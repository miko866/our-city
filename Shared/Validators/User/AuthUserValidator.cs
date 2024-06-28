using FluentValidation;
using FluentValidation.Results;
using Shared.InputModels;
using Shared.InputModels.User;

namespace Shared.Validators.UserValidator;

public class AuthUserValidator : AbstractValidator<LoginDetailsInputModel>
{
    public AuthUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().When(x => string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.Email).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName));
        RuleFor(x => x.Password).NotEmpty();
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue =>
        async (model, propertyName) =>
        {
            ValidationResult? result = await ValidateAsync(
                ValidationContext<LoginDetailsInputModel>.CreateWithOptions(
                    (LoginDetailsInputModel)model,
                    x => x.IncludeProperties(propertyName)
                )
            );
            if (result.IsValid)
                return Array.Empty<string>();

            return result.Errors.Select(e => e.ErrorMessage);
        };
}
