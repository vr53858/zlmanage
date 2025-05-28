using FluentValidation;
using FluentValidation.Results;

namespace ZLManage.DomainModel.Validation;

public static class ValidationExtensions
{
    public static void Validate<T>(this IValidator<T> validator, T instance)
    {
        if (validator is null)
            throw new ArgumentNullException(nameof(validator));

        ValidationResult result = validator.Validate(instance);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

            throw new ValidationException($"Validation failed:{Environment.NewLine}{string.Join(Environment.NewLine, errors)}", result.Errors);
        }
    }
}