using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : TaughtSubjectBaseModel
    {
        public IndexModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGetAsync()
        {
            CurrentRole.IsAdmin = true;
            GetAllTaughtSubjectsCommand command = new GetAllTaughtSubjectsCommand();
            TaughtSubjects = await mediator.Send(command);
        }
    }
}
