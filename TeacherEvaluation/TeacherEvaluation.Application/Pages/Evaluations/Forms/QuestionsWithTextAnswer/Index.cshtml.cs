using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TeacherEvaluation.Application.Pages.Evaluations.Forms.QuestionsWithTextAnswer
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
        }
    }
}
