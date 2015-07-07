using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Attribute
{
    public class ValidEnumAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var enumType = value.GetType();
            var valid = Enum.IsDefined(enumType, value);
            return !valid ? new ValidationResult(ErrorMessage ?? string.Format("Invalid value assigned for {0}.", validationContext.DisplayName)) : ValidationResult.Success;
        }
    }
}
