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

        public async Task<JsonResult> OnGetReturnSubjectsByStudent(string studentId)
        {
            if (!string.IsNullOrEmpty(studentId))
            {
                GetUserIdForStudentCommand getUserIdCommand = new GetUserIdForStudentCommand { StudentId = new Guid(studentId) };
                try
                {
                    Guid userIdStudent = await mediator.Send(getUserIdCommand);

                    var command = new GetSubjectsForInProgressEnrollmentsCommand
                    {
                        UserId = userIdStudent
                    };
                    var subjects = await mediator.Send(command);

                    return new JsonResult(subjects);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }

        public async Task<JsonResult> OnGetReturnSubjectsBySpecializationAndStudyYear(string specializationId, string studyYear)
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
                    var subjects = await mediator.Send(getSubjectsCommand);
                    return new JsonResult(subjects);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }
    }
}
