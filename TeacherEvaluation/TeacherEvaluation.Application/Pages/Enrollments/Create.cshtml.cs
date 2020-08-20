using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments;
using TeacherEvaluation.BusinessLogic.Commands.Students;
using TeacherEvaluation.BusinessLogic.Commands.Subjects;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [Required(ErrorMessage = "Student is required")]
        public Guid StudentId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid SubjectId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Teacher is required")]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        public TaughtSubjectType Type { get; set; }

        public List<SelectListItem> Students { get; set; }

        public List<SelectListItem> Subjects { get; set; }

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        {
            GetAllStudentsCommand getStudentsCommand = new GetAllStudentsCommand();
            var students = await mediator.Send(getStudentsCommand);

            GetAllSubjectsCommand getSubjectsCommand = new GetAllSubjectsCommand();
            var subjects = await mediator.Send(getSubjectsCommand);

            Students = students.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.User.FirstName + " " + x.User.LastName
                                            }).ToList();

            Subjects = subjects.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Name
                                            }).ToList();
            return Page();
        }

        public IActionResult OnGetCheckEnrollmentExistence(string studentId, string subjectId, string teacherId, string type)
        {
            CheckEnrollmentExistenceCommand command = new CheckEnrollmentExistenceCommand
            {
                TeacherId = new Guid(teacherId),
                StudentId = new Guid(studentId),
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            bool enrollmentExists = mediator.Send(command).Result;
            if (enrollmentExists)
            {
                return new JsonResult("The enrollment already exists");
            }
            return new JsonResult("This enrollment is available");
        }

        public IActionResult OnGetReturnTeachers(string subjectId, string type)
        {
            GetTeachersForSubjectCommand command = new GetTeachersForSubjectCommand
            {
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            IEnumerable<Teacher> teachers = new List<Teacher>();
            teachers = mediator.Send(command).Result;
            var toBeReturned = teachers.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.User.FirstName + " " + x.User.LastName
                                            }).ToList();
            return new JsonResult(teachers);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                EnrollStudentCommand command = new EnrollStudentCommand
                {
                    TeacherId = TeacherId,
                    SubjectId = SubjectId,
                    StudentId = StudentId,
                    Type = Type
                };
                try
                {
                    await mediator.Send(command);
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }

                return RedirectToPage("../Enrollments/Index");
            }
            return Page();
        }
    }
}
