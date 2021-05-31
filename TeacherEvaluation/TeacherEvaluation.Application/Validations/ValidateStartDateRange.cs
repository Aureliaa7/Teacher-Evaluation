using System;
using System.ComponentModel.DataAnnotations;

namespace TeacherEvaluation.Application.Validations
{
    public class ValidateStartDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime startDate = (DateTime)value;

            if (startDate >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("You can't set past dates!");
            }
        }
    }
}
