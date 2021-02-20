using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : TaughtSubjectBaseModelModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
        }

        public async Task OnGetAsync()
        {
            CurrentRole = new CurrentRole();
            CurrentRole.IsAdmin = true;
            GetAllTaughtSubjectsCommand command = new GetAllTaughtSubjectsCommand();
            TaughtSubjects = await mediator.Send(command);
        }
    }
}
