using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize]
    public class DetailsModel : TaughtSubjectBaseModel
    {
        public DetailsModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            TaughtSubjectId = (Guid)id;
            GetTaughtSubjectByIdCommand command = new GetTaughtSubjectByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                TaughtSubject taughtSubject = await mediator.Send(command);
                SetDetails(taughtSubject);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}
