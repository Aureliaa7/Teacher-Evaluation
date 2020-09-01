using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Accounts;

namespace TeacherEvaluation.Application.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly IMediator mediator;
        public List<string> ErrorMessages { get; set; }

        public ConfirmEmailModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync(string id, string token)
        {
            ConfirmEmailCommand confirmEmailCommand = new ConfirmEmailCommand { UserId = id, Token = token };
            ErrorMessages = await mediator.Send(confirmEmailCommand);
            return Page();
        }
    }
}