using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid EnrollmentId { get; set; }

        [BindProperty]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [BindProperty]
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }

        [BindProperty]
        [Display(Name = "Subject Title")]
        public string SubjectTitle { get; set; }

        [BindProperty]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Display(Name = "Study Year")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        [Display(Name = "Study Programme")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        public string Section { get; set; }

        [BindProperty]
        public string Group { get; set; }


        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            EnrollmentId = (Guid)id;
            GetEnrollmentByIdCommand command = new GetEnrollmentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Enrollment enrollment = await mediator.Send(command);
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
                Section = enrollment.Student.Section;
                StudyProgramme = enrollment.Student.StudyProgramme;
                StudyYear = enrollment.Student.StudyYear;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}
