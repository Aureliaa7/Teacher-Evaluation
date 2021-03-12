using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class TaughtSubjectBaseModel : PageModel
    {
        protected IMediator mediator;

        public IEnumerable<TaughtSubject> TaughtSubjects { get; set; }
        public CurrentRole CurrentRole { get; set; }

        [BindProperty]
        public Guid TaughtSubjectId { get; set; }

        [BindProperty]
        [Display(Name = "Teacher")]
        public string TeacherName { get; set; }

        [BindProperty]
        [Display(Name = "Subject")]
        public string SubjectTitle { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        [Required(ErrorMessage = "Type is required")]
        public TaughtSubjectType? Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Teacher is required")]
        public Guid? TeacherId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid? SubjectId { get; set; }

        [BindProperty]
        [EnumDataType(typeof(Department))]
        [Required(ErrorMessage = "Department is required")]
        public Department? Department { get; set; }

        public TaughtSubjectBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
            CurrentRole = new CurrentRole();
        }
    }
}
