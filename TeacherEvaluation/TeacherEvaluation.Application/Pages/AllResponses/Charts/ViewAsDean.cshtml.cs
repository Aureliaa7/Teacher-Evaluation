using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeacherEvaluation.Application.Pages.AllResponses.Charts
{
    [Authorize(Roles = "Dean")]
    public class ViewAsDeanModel : ViewChartsBaseModel
    {
        [BindProperty]
        public List<SelectListItem> Teachers { get; set; } = new List<SelectListItem>();

        public ViewAsDeanModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGetAsync(Guid formId)
        {
            FormId = formId;
            Teachers = await GetAllTeachersAsync();
        }
    }
}
