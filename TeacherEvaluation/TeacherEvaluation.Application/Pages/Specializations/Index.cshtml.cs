using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<JsonResult> OnGet(string studyDomainId)
        {
            if (!string.IsNullOrEmpty(studyDomainId))
            {
                try
                {
                    GetSpecializationsByDomainCommand command = new GetSpecializationsByDomainCommand { StudyDomainId = new Guid(studyDomainId) };
                    var specializations = await mediator .Send(command);
                    return new JsonResult(specializations.OrderBy(x => x.Name));
                }
                catch(ItemNotFoundException) { }
            }
            return new JsonResult("");
        }
    }
}
