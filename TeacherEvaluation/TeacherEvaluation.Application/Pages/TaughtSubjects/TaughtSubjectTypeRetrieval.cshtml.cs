using System;
using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class TaughtSubjectTypeRetrievalModel : PageModel
    {
        private readonly IMediator mediator;

        public TaughtSubjectTypeRetrievalModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet(string subjectId)
        {
            Guid currentTeacherId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetSubjectsForTeacherCommand command = new GetSubjectsForTeacherCommand { UserId = currentTeacherId };
            var allTaughtSubjects = mediator.Send(command).Result;
            var taughtSubjects = allTaughtSubjects.Where(x => x.Subject.Id == new Guid(subjectId));
            return new JsonResult(taughtSubjects);
        }
    }
}
