using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Students
{
    public class StudentBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        [Display(Name = "Student")]
        public Guid? StudentId { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Personal identification number is required")]
        [RegularExpression(pattern: "^[1-9]\\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01])(0[1-9]|[1-4]\\d|5[0-2]|99)(00[1-9]|0[1-9]\\d|[1-9]\\d\\d)\\d$", ErrorMessage = "Invalid CNP")]
        [MaxLength(13)]
        public string PIN { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(3)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(3)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Father's initial is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+$", ErrorMessage = "Invalid text")]
        [MaxLength(2)]
        [Display(Name = "Father's initial")]
        public string FathersInitial { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "Study year must be between 1 and 4")]
        [Display(Name = "Study year")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        [Display(Name = "Study programme")]
        public StudyProgramme? StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        [Display(Name = "Study domain")]
        public Guid? StudyDomainId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Specialization is required")]
        [Display(Name = "Specialization")]
        public Guid? SpecializationId { get; set; }

        [BindProperty]
        public Specialization Specialization { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Group is required")]
        [RegularExpression(pattern: "^[a-zA-Z1-4\\.1-4a-zA-Z]+$", ErrorMessage = "Invalid text")]
        public string Group { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public CurrentRole CurrentRole { get; set; }

        public StudentBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
            Students = new List<Student>();
            CurrentRole = new CurrentRole();
        }
    }
}
