using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Evaluations.Forms.QuestionsWithOptionAnswer
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public List<string> Questions { get; set; }

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CreateFormCommand command = new CreateFormCommand
                {
                    FormType = FormType.Option,
                    Questions = Questions
                };
                await mediator.Send(command);
                return RedirectToPage("/Dashboards/Dean");
            }
            else
            {
                return Page();
            }
        }

        public void OnPostAddQuestion(string question)
        {
            Questions.Add(question);
        }
    }
}