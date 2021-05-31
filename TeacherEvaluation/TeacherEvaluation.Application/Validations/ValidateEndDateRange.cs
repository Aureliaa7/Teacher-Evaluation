using System;
using System.ComponentModel.DataAnnotations;
using TeacherEvaluation.Application.Pages.EvaluationForms;

namespace TeacherEvaluation.Application.Validations
{
    public class ValidateEndDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CreateModel)validationContext.ObjectInstance;
            DateTime endDate = (DateTime)value;

            if (endDate > model.StartDate.AddDays(2))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("End date must be greater than start date!");
            }
        }
    }
}
