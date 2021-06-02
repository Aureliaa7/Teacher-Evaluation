using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;

/// <summary>
/// It is used for the second dropdown(the one with courses and laboratories)
/// from the ViewCharts page
/// </summary>

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class TaughtSubjectsByTeacherIdModel : PageModel
    {
        private readonly IMediator mediator;

        public TaughtSubjectsByTeacherIdModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<JsonResult> OnGet(string teacherId)
        {
            if (!string.IsNullOrEmpty(teacherId))
            {
                TaughtSubjectsByTeacherCommand command = new TaughtSubjectsByTeacherCommand
                {
                    TeacherId = new System.Guid(teacherId)
                };
                var taughtSubjectsIdsAndTitles = await mediator.Send(command);
                return new JsonResult(taughtSubjectsIdsAndTitles);

            }
            return new JsonResult("");
        }
    }
}
