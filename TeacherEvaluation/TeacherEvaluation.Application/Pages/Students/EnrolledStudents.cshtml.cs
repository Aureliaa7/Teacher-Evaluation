using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Teacher")]
    public class EnrolledStudentsModel : StudentBaseModel
    {
        public EnrolledStudentsModel(IMediator mediator): base(mediator)
        {
        }

        public async Task OnGet(Guid? id)
        {
            GetCurrentlyEnrolledStudentsCommand command = new GetCurrentlyEnrolledStudentsCommand { TaughtSubjectId = (Guid)id };
            try
            {
                Students = await mediator.Send(command);
            }
            catch (ItemNotFoundException) { }
        }
    }
}