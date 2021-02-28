using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : SubjectBaseModel
    {
        public CreateModel(IMediator mediator): base(mediator)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                AddSubjectCommand command = new AddSubjectCommand
                {
                    Name = SubjectName,
                    NumberOfCredits = (int)NumberOfCredits
                };
                await mediator.Send(command);
                return RedirectToPage("../Subjects/Index");
            }
            return Page();
        }
    }
}
