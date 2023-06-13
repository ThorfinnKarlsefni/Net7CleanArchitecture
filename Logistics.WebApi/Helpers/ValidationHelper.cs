using System;
using FluentValidation;
using FluentValidation.Results;

namespace Logistics.WebApi.Helpers
{
    public abstract class ValidationHelper<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);

            if (!result.IsValid)
            {
                var errorDetails = new ValidationException(result.Errors);
                throw errorDetails;
            }

            return result;
        }
    }
}

