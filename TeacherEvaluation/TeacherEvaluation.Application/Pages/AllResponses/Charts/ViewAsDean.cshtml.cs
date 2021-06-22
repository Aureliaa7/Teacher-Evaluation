using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace TeacherEvaluation.Application.Pages.AllResponses.Charts
{
    [Authorize(Roles = "Dean")]
    public class ViewAsDeanModel : ViewChartsBaseModel
    {
        public ViewAsDeanModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGetAsync(Guid formId)
        {
            FormId = formId;
            JSFunctionToBeCalled = $"draw_charts_and_tag_cloud('{FormId}', 'mainElementDeanLayout')";
            Teachers = await GetAllTeachersAsync();
        }
    }
}
