using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<Subject> Subjects;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            Subjects = new List<Subject>();
        }

        public async Task OnGetAsync()
        {
            GetAllSubjectsCommand command = new GetAllSubjectsCommand();
            Subjects = await mediator.Send(command);
        }

        public IActionResult OnGetReturnSubjectsByStudent(string studentId)
        {
            if (!string.IsNullOrEmpty(studentId))
            {
                GetUserIdForStudentCommand getUserIdCommand = new GetUserIdForStudentCommand { StudentId = new Guid(studentId) };
                try
                {
                    Guid userIdStudent = mediator.Send(getUserIdCommand).Result;
                    GetSubjectsForEnrollmentsCommand getSubjectsCommand = new GetSubjectsForEnrollmentsCommand
                    {
                        UserId = userIdStudent,
                        EnrollmentState = EnrollmentState.InProgress
                    };
                    var subjects = mediator.Send(getSubjectsCommand).Result;
                    return new JsonResult(subjects);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }

        public IActionResult OnGetReturnSubjectsBySpecializationAndStudyYear(string specializationId, string studyYear)
        {
            if (!string.IsNullOrEmpty(specializationId) && !string.IsNullOrEmpty(studyYear))
            {
                try
                {
                    GetSubjectsBySpecializationIdAndStudyYearCommand getSubjectsCommand = new GetSubjectsBySpecializationIdAndStudyYearCommand
                    {
                        SpecializationId = new Guid(specializationId),
                        StudyYear = Int32.Parse(studyYear)
                    };
                    var subjects = mediator.Send(getSubjectsCommand).Result;
                    return new JsonResult(subjects);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }
    }
}
