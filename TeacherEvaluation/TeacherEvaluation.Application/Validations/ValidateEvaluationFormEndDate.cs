using System;
using System.ComponentModel.DataAnnotations;
using TeacherEvaluation.Application.Pages.EvaluationForms;
using TeacherEvaluation.BusinessLogic;

namespace TeacherEvaluation.Application.Validations
{
    public class ValidateEvaluationFormEndDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CreateModel)validationContext.ObjectInstance;
            DateTime endDate = (DateTime)value;

            if (endDate > model.StartDate.AddDays(Constants.MinNumberOfDaysToEvaluateTeachers))
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
