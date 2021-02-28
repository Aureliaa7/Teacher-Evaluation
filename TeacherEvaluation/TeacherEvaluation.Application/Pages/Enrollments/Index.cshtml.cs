using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    [Authorize]
    public class IndexModel : EnrollmentBaseModel
    {
        public IndexModel(IMediator mediator): base(mediator)
        {
        }

        public async Task OnGetAsync()
        {
            GetAllEnrollmentsCommand command = new GetAllEnrollmentsCommand();
            Enrollments = await mediator.Send(command);
        }

        public IActionResult OnGetReturnEnrolledStudents(string subjectId, string type)
        {
            Guid currentTeacherId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetEnrolledStudentsCommand command = new GetEnrolledStudentsCommand
            {
                UserId = currentTeacherId,
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            var students = mediator.Send(command).Result;
            return new JsonResult(students);
        }

        public IActionResult OnGetCheckEnrollmentExistenceBySubjectStudent(string studentId, string subjectId, string type)
        {
            EnrollmentExistsCommand command = new EnrollmentExistsCommand
            {
                StudentId = new Guid(studentId),
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            bool enrollmentExists = mediator.Send(command).Result;
            if (enrollmentExists)
            {
                return new JsonResult("The enrollment exists");
            }
            return new JsonResult("The enrollment does not exist");
        }

        public IActionResult OnGetCheckEnrollmentExistenceByStudentSubjectTeacherType(string studentId, string subjectId, string teacherId, string type)
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
    }
}
