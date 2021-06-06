using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TeacherEvaluation.Application.Pages.Shared;
using TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Grades
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

            var command = new InsertGradesFromExcelFileCommand
            {
                ExcelFile = ExcelFile
            };

            try
            {
                ErrorMessages = await mediator.Send(command);
                if (ErrorMessages.Count > 0)
                {
                    return Page();
                }
                return RedirectToPage("../Grades/SearchByCriteria");
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
