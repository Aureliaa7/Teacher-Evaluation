using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject;

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
            GetStudentsForSubjectCommand command = new GetStudentsForSubjectCommand { TaughtSubjectId = (Guid)id };
            Students = await mediator.Send(command);
        }
    }
}