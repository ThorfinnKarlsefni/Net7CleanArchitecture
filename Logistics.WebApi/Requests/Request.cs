using System;
using FluentValidation;
using FluentValidation.Results;

using ValidationException = FluentValidation.ValidationException;

namespace Logistics.WebApi.Requests
{
    public abstract class Requests<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return result;
        }
    }
}

