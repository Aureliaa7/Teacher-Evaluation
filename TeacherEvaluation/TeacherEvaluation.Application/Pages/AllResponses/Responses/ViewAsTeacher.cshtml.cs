using MediatR;
using System;
using System.Security.Claims;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.AllResponses.Responses
{
    public class ViewAsTeacherModel : ViewResponsesBaseModel
    {
        public ViewAsTeacherModel(IMediator mediator) :  base(mediator)
        {
        }

        public async void OnGet(Guid formId)
        {
            FormId = formId;
            GetTeacherIdByUserIdCommand getTeacherIdCommand = new GetTeacherIdByUserIdCommand
            {
                UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            try
            {
                TeacherId = await mediator.Send(getTeacherIdCommand);
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }
    }
}
