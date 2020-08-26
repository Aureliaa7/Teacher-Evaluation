using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
        }

        public IEnumerable<TaughtSubject> TaughtSubjects { get; set; }

        public async Task OnGetAsync()
        {
            GetAllTaughtSubjectsCommand command = new GetAllTaughtSubjectsCommand();
            TaughtSubjects = await mediator.Send(command);
        }
    }
}
