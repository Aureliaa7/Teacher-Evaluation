using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

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
            try
            {
                GetAllTeachersCommand getTeachersCommand = new GetAllTeachersCommand();
                var teachers = await mediator.Send(getTeachersCommand);


                Teachers = teachers.Select(x =>
                                                new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.User.FirstName + " " + x.User.LastName
                                                }).ToList();
            }
            catch (ItemNotFoundException) { }
        }
    }
}
