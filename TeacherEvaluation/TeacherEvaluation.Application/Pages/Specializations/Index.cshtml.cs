using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Specializations
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet(string studyDomainId)
        {
            if (!string.IsNullOrEmpty(studyDomainId))
            {
                try
                {
                    GetSpecializationsByDomainCommand command = new GetSpecializationsByDomainCommand { StudyDomainId = new Guid(studyDomainId) };
                    var specializations = mediator.Send(command).Result;
                    return new JsonResult(specializations);
                }
                catch(ItemNotFoundException) { }
            }
            return new JsonResult("");
        }
    }
}
