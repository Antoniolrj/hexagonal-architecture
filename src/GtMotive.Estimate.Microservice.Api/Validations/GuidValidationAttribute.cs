using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Validations
{
    public sealed class GuidValidationAttribute : ValidationAttribute
    {
        public GuidValidationAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string stringValue)
            {
                return Guid.TryParse(stringValue, out _);
            }

            return false;
        }
    }
}
