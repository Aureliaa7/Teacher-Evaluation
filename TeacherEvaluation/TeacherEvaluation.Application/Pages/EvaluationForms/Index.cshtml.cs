using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public IEnumerable<Form> Forms { get; set; }

        public async Task OnGetAsync()
        {
            GetEvaluationFormsCommand command = new GetEvaluationFormsCommand();
            Forms = await mediator.Send(command);
        }
    }
}
