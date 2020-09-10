using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [Required(ErrorMessage = "Subject title is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(5)]
        public string SubjectName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Number of credits is required")]
        [Range(1,5, ErrorMessage ="Number of credits must be between 1 and 5")]
        public int? NumberOfCredits { get; set; } = null;

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
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
