using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeacherEvaluation.Application.Pages.AllResponses.Responses
{
    [Authorize(Roles = "Dean")]
    public class ViewAsDeanModel : ViewResponsesBaseModel
    {
        [BindProperty]
        public List<SelectListItem> Teachers { get; set; } = new List<SelectListItem>();

        public ViewAsDeanModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGet(Guid formId)
        {
            FormId = formId;
            Teachers = await GetAllTeachersAsync();
        }
    }
}
