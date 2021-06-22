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
        [Display(Name = "Student")]
        public Guid? StudentId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        [Display(Name = "Subject")]
        public Guid? SubjectId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Teacher is required")]
        [Display(Name = "Teacher")]
        public Guid? TeacherId { get; set; }

        [BindProperty]
        [Display(Name = "Student name")]
        public string StudentName { get; set; }

        [BindProperty]
        [Display(Name = "Teacher name")]
        public string TeacherName { get; set; }

        [BindProperty]
        [Display(Name = "Subject title")]
        public string SubjectTitle { get; set; }

        [BindProperty]
        public TaughtSubjectType? Type { get; set; }

        [BindProperty]
        [Display(Name = "Study year")]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        [Display(Name = "Study programme")]
        [Required(ErrorMessage = "Study programme is required")]
        public StudyProgramme? StudyProgramme { get; set; }

        [BindProperty]
        [Display(Name = "Study domain")]
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
        [Display(Name = "Study domain")]
        public Guid? StudyDomainId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Specialization is required")]
        [Display(Name = "Specialization")]
        public Guid? SpecializationId { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }

        public EnrollmentBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
            Enrollments = new List<Enrollment>();
        }

        protected void InitializeDetails(Enrollment enrollment)
        {
            string studentFirstName = enrollment.Student.User.FirstName;
            string studentFathersInitial = enrollment.Student.User.FathersInitial;
            string studentLastName = enrollment.Student.User.LastName;

            string teacherFirstName = enrollment.TaughtSubject.Teacher.User.FirstName;
            string teacherFathersInitial = enrollment.TaughtSubject.Teacher.User.FathersInitial;
            string teacherLastName = enrollment.TaughtSubject.Teacher.User.LastName;

            TeacherName = string.Join(" ", teacherLastName, teacherFathersInitial, teacherFirstName);
            SubjectTitle = enrollment.TaughtSubject.Subject.Name;
            Type = enrollment.TaughtSubject.Type;
            StudentName = string.Join(" ", studentLastName, studentFathersInitial, studentFirstName);
            Group = enrollment.Student.Group;
            Specialization = enrollment.Student.Specialization.Name;
            StudyDomain = enrollment.Student.Specialization.StudyDomain.Name;
            StudyProgramme = enrollment.Student.Specialization.StudyDomain.StudyProgramme;
            StudyYear = enrollment.Student.StudyYear;
        }
    }
}
