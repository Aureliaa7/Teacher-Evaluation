using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : StudentBaseModel
    {
        public IndexModel(IMediator mediator): base(mediator)
        {
        }

        public async Task OnGetAsync()
        {
            GetAllStudentsCommand command = new GetAllStudentsCommand();
            Students = await mediator.Send(command);
            CurrentRole.IsAdmin = true;
        }
    }
}
