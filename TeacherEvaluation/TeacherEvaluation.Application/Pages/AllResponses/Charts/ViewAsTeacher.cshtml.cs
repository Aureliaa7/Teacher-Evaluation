using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.AllResponses.Charts
{
    [Authorize(Roles = "Teacher")]
    public class ViewAsTeacherModel : ViewChartsBaseModel
    {
        public ViewAsTeacherModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGetAsync(Guid formId)
        {
            FormId = formId;
            GetTeacherIdByUserIdCommand getTeacherIdCommand = new GetTeacherIdByUserIdCommand
            {
                UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            try
            {
                TeacherId = await mediator.Send(getTeacherIdCommand);
                JSFunctionToBeCalled = $"draw_charts_and_tag_cloud('{FormId}', 'mainElementTeacherLayout')";
            }
            catch (ItemNotFoundException)
            {
            }
        }
    }
}
