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
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : StudentBaseModel
    {
        public DeleteModel(IMediator mediator): base(mediator)
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
                Student studentToBeDeleted = await mediator.Send(command);
                InitializeDetails(studentToBeDeleted);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (StudentId != null)
                {
                    DeleteStudentCommand command = new DeleteStudentCommand
                    {
                        Id = (Guid)StudentId
                    };
                    await mediator.Send(command);
                }
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return RedirectToPage("../Students/Index");
        }
    }
}
