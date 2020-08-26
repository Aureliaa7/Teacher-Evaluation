using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            Enrollments = new List<Enrollment>();
        }

        public IEnumerable<Enrollment> Enrollments { get;set; }

        public async Task OnGetAsync()
        {
            GetAllEnrollmentsCommand command = new GetAllEnrollmentsCommand();
            Enrollments = await mediator.Send(command);
        }
    }
}
