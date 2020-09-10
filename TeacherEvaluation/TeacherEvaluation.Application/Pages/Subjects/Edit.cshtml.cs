using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid SubjectId { get; set; }
        [BindProperty]
        [Display(Name = "Subject title")]
        [Required(ErrorMessage = "Subject title is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        public string SubjectName { get; set; }
        [BindProperty]
        [Display(Name = "Number of credits")]
        [Required(ErrorMessage = "Number of credits is required")]
        [Range(1, 5, ErrorMessage = "Number of credits must be between 1 and 5")]
        public int NumberOfCredits { get; set; }

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if(id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            SubjectId = (Guid)id;
            GetSubjectByIdCommand command = new GetSubjectByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Subject subject = await mediator.Send(command);
                SubjectName = subject.Name;
                NumberOfCredits = subject.NumberOfCredits;
            }
            catch(ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateSubjectCommand command = new UpdateSubjectCommand
                    {
                        Id = SubjectId,
                        Name = SubjectName,
                        NumberOfCredits = NumberOfCredits
                    };
                    await mediator.Send(command);
                    return RedirectToPage("../Subjects/Index");
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }
            }
            return Page();
        }
    }
}
