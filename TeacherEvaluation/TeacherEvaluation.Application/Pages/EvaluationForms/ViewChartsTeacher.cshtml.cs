using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    [Authorize(Roles = "Teacher")]
    public class ViewChartsTeacherModel : ViewChartsBaseModel
    {
        public ViewChartsTeacherModel(IMediator mediator) : base(mediator)
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
            }
            catch(ItemNotFoundException)
            {
            }
        }
    }
}
