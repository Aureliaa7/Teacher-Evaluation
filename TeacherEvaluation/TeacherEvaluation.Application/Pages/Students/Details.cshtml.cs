using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize]
    public class DetailsModel : StudentBaseModel
    {
        public DetailsModel(IMediator mediator): base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            StudentId = (Guid)id;
            GetStudentByIdCommand command = new GetStudentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Student student = await mediator.Send(command);
                InitializeDetails(student);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}
