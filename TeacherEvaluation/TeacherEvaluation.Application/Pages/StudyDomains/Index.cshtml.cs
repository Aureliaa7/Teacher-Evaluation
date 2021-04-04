using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.StudyDomains
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet(string studyProgramme)
        {
            if (!string.IsNullOrEmpty(studyProgramme))
            {
                try
                {
                    StudyProgramme programme = (StudyProgramme)Enum.Parse(typeof(StudyProgramme), studyProgramme);
                    GetStudyDomainsByProgrammeCommand command = new GetStudyDomainsByProgrammeCommand { StudyProgramme = programme };
                    var domains = mediator.Send(command).Result;
                    return new JsonResult(domains.OrderBy(x => x.Name));
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }
    }
}
