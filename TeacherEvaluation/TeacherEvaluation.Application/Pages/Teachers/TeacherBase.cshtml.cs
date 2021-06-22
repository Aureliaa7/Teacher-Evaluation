using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class TeacherBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        public TeacherBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        [Display(Name = "Teacher")]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Personal identification number is required")]
        [RegularExpression(pattern: "^[1-9]\\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01])(0[1-9]|[1-4]\\d|5[0-2]|99)(00[1-9]|0[1-9]\\d|[1-9]\\d\\d)\\d$", ErrorMessage = "Invalid CNP")]
        [MaxLength(13)]
        public string PIN { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [MinLength(3)]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Father's initial is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+$", ErrorMessage = "Invalid text")]
        [Display(Name = "Father's initial")]
        [MaxLength(2)]
        public string FathersInitial { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Degree is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(2)]
        public string Degree { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Department is required")]
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        protected void InitializeDetails(Teacher teacher)
        {
            FirstName = teacher.User.FirstName;
            LastName = teacher.User.LastName;
            Email = teacher.User.Email;
            FathersInitial = teacher.User.FathersInitial;
            PIN = teacher.User.PIN;
            Degree = teacher.Degree;
            Department = teacher.Department;
        }
    }
}
