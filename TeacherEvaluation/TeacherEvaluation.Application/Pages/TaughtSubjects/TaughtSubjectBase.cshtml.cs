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
        [Display(Name = "Taught subject")]
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
        [Display(Name = "Taught subject type")]
        public TaughtSubjectType? Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Teacher is required")]
        [Display(Name = "Teacher")]
        public Guid? TeacherId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        [Display(Name = "Subject")]
        public Guid? SubjectId { get; set; }

        [BindProperty]
        [EnumDataType(typeof(Department))]
        [Required(ErrorMessage = "Department is required")]
        public Department? Department { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Max number of attendances is required")]
        [Display(Name = "Max number of attendances")]
        public int? MaxNumberOfAttendances { get; set; } = null;

        [BindProperty]
        public Semester Semester { get; set; }

        [BindProperty]
        public string Specialization { get; set; }

        [BindProperty]
        [Display(Name = "Study programme")]
        public string StudyProgramme { get; set; }

        [BindProperty]
        [Display(Name = "Study domain")]
        public string StudyDomain { get; set; }

        public TaughtSubjectBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
            CurrentRole = new CurrentRole();
        }

        protected void SetDetails(TaughtSubject taughtSubject)
        {
            TeacherName = taughtSubject.Teacher.User.FirstName + " " + taughtSubject.Teacher.User.LastName;
            SubjectTitle = taughtSubject.Subject.Name;
            Type = taughtSubject.Type;
            Semester = taughtSubject.Subject.Semester;
            Specialization = taughtSubject.Subject.Specialization.Name;
            StudyProgramme = taughtSubject.Subject.Specialization.StudyDomain.StudyProgramme.ToString();
            StudyDomain = taughtSubject.Subject.Specialization.StudyDomain.Name;
        }
    }
}
