using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.Application.Pages.Shared;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    [Authorize(Roles = "Administrator")]
    public class InsertFromFileModel : InsertFromFileBaseModel
    {
        public InsertFromFileModel(IMediator mediator) : base(mediator)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string confirmationUrlTemplate = Url.Page(
                       "/Account/ConfirmEmail",
                       pageHandler: null,
                       values: new { id = "((userId))", token = "((token))" },
                       protocol: Request.Scheme);

            var command = new InsertTeachersFromFileCommand
            {
                ExcelFile = ExcelFile,
                ConfirmationUrlTemplate = confirmationUrlTemplate
            };
            try
            {
                ErrorMessages = await mediator.Send(command);
                if (ErrorMessages.Count > 0)
                {
                    return Page();
                }
                return RedirectToPage("../Teachers/Index");
            }
            catch (ItemNotFoundException ex)
            {
                ErrorMessages.Add(ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMessages.Add(ex.Message);
            }
            return Page();
        }
    }
}
