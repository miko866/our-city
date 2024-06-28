using System.Globalization;
using FluentValidation;
using Shared.Extensions;
using Shared.Helpers;

namespace Shared.Validators;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsCountryISO3166Alpha2Format<T>(
        this IRuleBuilder<T, string> ruleBuilder
    )
    {
        return ruleBuilder
            .Must(x =>
                CultureInfo
                    .GetCultures(CultureTypes.SpecificCultures)
                    .Select(cultureInfo => new RegionInfo(cultureInfo.Name))
                    .Count(region => region.TwoLetterISORegionName == x) >= 1
            )
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_ISO_3166);
        // .WithMessage("'{PropertyName}' needs to be in ISO-3166 Alpha-2 format.");
    }

    public static IRuleBuilderOptions<T, string> NotStartWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(m => m != null && !m.StartsWith(" "))
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NO_START_WHITESPACES);
        // .WithMessage("'{PropertyName}' should not start with whitespace");
    }

    public static IRuleBuilderOptions<T, string> NotEndWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(m => m != null && !m.EndsWith(" "))
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NO_END_WHITESPACES);
        // .WithMessage("'{PropertyName}' should not end with whitespace");
    }

    public static IRuleBuilderOptions<T, string> NoWhiteSpaces<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(m => m != null && !m.Contains(" "))
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NO_WHITESPACES);
        // .WithMessage("'{PropertyName}' should not contain any whitespaces");
    }

    public static IRuleBuilderOptions<T, string> NullOrNotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(m => m == null || m.Length > 0)
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY);
        //.WithMessage("'{PropertyName}' should be null or not empty");
    }

    public static IRuleBuilderOptions<T, string> NullOrNotEmptyMinSize<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minSize
    )
    {
        return ruleBuilder
            .Must(m => m == null || m.Length >= minSize)
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY_SIZE_BIGGER);
        // .WithMessage("'{PropertyName}' should be null or not empty and bigger than " + minSize.ToString() + " chars");
    }

    public static IRuleBuilderOptions<T, string> NullOrNotEmptyMaxSize<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int maxSize
    )
    {
        return ruleBuilder
            .Must(m => m == null || m.Length <= maxSize)
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY_SIZE_SMALLER);
        // .WithMessage("'{PropertyName}' should be null or not empty and smaller than " + maxSize.ToString() + " chars");
    }

    public static IRuleBuilderOptions<T, string> NullOrNotEmptySized<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minSize,
        int MaxSize
    )
    {
        return ruleBuilder
            .Must(m => m == null || m.Length >= minSize && m.Length <= MaxSize)
            .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY_SIZE_BETWEEN);
        // .WithMessage("'{PropertyName}' should be null or not empty and between " + minSize.ToString() + " and " + MaxSize.ToString() + " chars");
    }

    public static IRuleBuilderOptions<T, DateTime?> IsNullOrNotOlderThan<T>(
        this IRuleBuilder<T, DateTime?> ruleBuilder,
        int maxAge
    )
    {
        return (IRuleBuilderOptions<T, DateTime?>)
            ruleBuilder
                .Must(m => m == null || m.ValidAge(maxAge))
                .WithErrorCode(ErrorCodes.CODE_VALIDATION_ERROR_INVALID_AGE);
        // .WithMessage("'{PropertyName}' is invalid age!");
    }
}
