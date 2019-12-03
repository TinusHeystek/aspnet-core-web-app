using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Example.Core.Extensions
{
    public static class ValidatorExtensions
    {
        public static IEnumerable<string> ToExceptionMassages(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(e =>
                "Property " + e.PropertyName + " failed validation. Error was: " + e.ErrorMessage);
        }
    }
}