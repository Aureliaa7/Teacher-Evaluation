using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace TeacherEvaluation.Application.Pages.AllResponses.Responses
{
    [Authorize(Roles = "Dean")]
    public class ViewAsDeanModel : ViewResponsesBaseModel
    {
        public ViewAsDeanModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGet(Guid formId)
        {
            FormId = formId;
            JSFunctionToBeCalled = $"get_responses('{FormId}', 'mainElementDeanLayout')";
            Teachers = await GetAllTeachersAsync();
        }
    }
}
