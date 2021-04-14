using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    public class SubjectBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        public SubjectBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        [Display(Name = "Subject title")]
        [Required(ErrorMessage = "Subject title is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        public string SubjectName { get; set; }
        
        [BindProperty]
        [Display(Name = "Number of credits")]
        [Required(ErrorMessage = "Number of credits is required")]
        [Range(1, 5, ErrorMessage = "Number of credits must be between 1 and 5")]
        public int? NumberOfCredits { get; set; }

        [BindProperty]
        [Display(Name = "Study year")]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 5, ErrorMessage = "Study year must be between 1 and 4")]
        public int? StudyYear { get; set; }

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        [Display(Name = "Study programme")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        [Display(Name = "Study domain")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Specialization is required")]
        public Guid SpecializationId { get; set; }

        public Specialization Specialization { get; set; }
    }
}
