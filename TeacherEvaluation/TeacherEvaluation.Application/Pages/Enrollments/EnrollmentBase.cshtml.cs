using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    public class EnrollmentBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        public Guid? EnrollmentId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Student is required")]
        public Guid? StudentId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid? SubjectId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Teacher is required")]
        public Guid? TeacherId { get; set; }

        [BindProperty]
        [Display(Name = "Student")]
        public string StudentName { get; set; }

        [BindProperty]
        [Display(Name = "Teacher")]
        public string TeacherName { get; set; }

        [BindProperty]
        [Display(Name = "Subject")]
        public string SubjectTitle { get; set; }

        [BindProperty]
        public TaughtSubjectType? Type { get; set; }

        [BindProperty]
        [Display(Name = "Study Year")]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        [Display(Name = "Study Programme")]
        [Required(ErrorMessage = "Study programme is required")]
        public StudyProgramme? StudyProgramme { get; set; }

        [BindProperty]
        [Display(Name = "Study Domain")]
        [Required(ErrorMessage = "Study domain is required")]
        public string StudyDomain { get; set; }

        [BindProperty]
        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; }

        [BindProperty]
        public string Group { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        public Guid? StudyDomainId { get; set; }

        [BindProperty]
        [Display(Name = "Study domain")]
        [Required(ErrorMessage = "Specialization is required")]
        
        public Guid? SpecializationId { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }

        public EnrollmentBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
            Enrollments = new List<Enrollment>();
        }
    }
}
