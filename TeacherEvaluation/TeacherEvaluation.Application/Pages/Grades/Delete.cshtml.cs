using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.Grades
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Guid GradeId { get; set; }

        [BindProperty]
        public GradeVm Grade { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            GradeId = id;
            var command = new GetGradeByIdCommand {
                Id = id
            };
            try
            {
                Grade = await mediator.Send(command);
                return Page();
            }
            catch(ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var command = new ResetGradeCommand
            {
                GradeId = GradeId
            };
            try
            {
                await mediator.Send(command);
                return RedirectToPage("../MyProfile/Administrator");
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
        }
    }
}
