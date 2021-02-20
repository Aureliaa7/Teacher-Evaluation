using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Evaluations.Forms.QuestionsWithOptionAnswer
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
            GetFormsByTypeCommand command = new GetFormsByTypeCommand { Type = FormType.Option };
            Forms = await mediator.Send(command);
        }
    }
}
