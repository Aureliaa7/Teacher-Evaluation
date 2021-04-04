using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Personal identification number is required")]
        [RegularExpression(pattern: "[1-9]([0-9]{12}$)", ErrorMessage = "Invalid text")]
        [MaxLength(13)]
        public string PIN { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [Display(Name = "First name")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
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
    }
}
