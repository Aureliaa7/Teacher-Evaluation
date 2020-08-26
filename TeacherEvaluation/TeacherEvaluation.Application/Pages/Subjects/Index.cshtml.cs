using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
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
    }
}
