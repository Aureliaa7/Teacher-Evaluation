using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    [Authorize(Roles = "Administrator")]
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

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Display(Name = "Study domain")]
        [Required(ErrorMessage = "Specialization is required")]
        public Guid SpecializationId { get; set; }

        [BindProperty]
        [Display(Name = "Study year")]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        public List<SelectListItem> Subjects { get; set; }

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        { 
            GetAllSubjectsCommand getSubjectsCommand = new GetAllSubjectsCommand();
            var subjects = await mediator.Send(getSubjectsCommand);
            Subjects = subjects.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Name
                                            }).ToList();
            return Page();
        }

        public IActionResult OnGetCheckEnrollmentAvailability(string studentId, string subjectId, string teacherId, string type)
        {
            CheckEnrollmentAvailabilityCommand command = new CheckEnrollmentAvailabilityCommand
            {
                TeacherId = new Guid(teacherId),
                StudentId = new Guid(studentId),
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            bool enrollmentIsAvailable = mediator.Send(command).Result;
            if (enrollmentIsAvailable)
            {
                return new JsonResult("This enrollment is available");
            }
            return new JsonResult("The enrollment is not available");
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
            return new JsonResult(teachers);
        }


        public IActionResult OnGetReturnStudents(string specializationId, string studyYear)
        {
            GetStudentsBySpecializationIdAndYearCommand command = new GetStudentsBySpecializationIdAndYearCommand
            {
                SpecializationId = new Guid(specializationId),
                StudyYear = int.Parse(studyYear)
            };
            var students = mediator.Send(command).Result;
            return new JsonResult(students);
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
