using FluentValidation;
using FluentValidation.Results;

public class CustomValidator<T> : AbstractValidator<T>
{
    public override ValidationResult Validate(ValidationContext<T> context)
    {
        var result = base.Validate(context);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }

        return result;
    }
}