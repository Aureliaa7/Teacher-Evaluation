using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class TaughtSubjectTypeRetrievalModel : PageModel
    {
        private readonly IMediator mediator;

        public TaughtSubjectTypeRetrievalModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// This is used by a logged in teacher
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnGet(string subjectId)
        {
            if (!string.IsNullOrEmpty(subjectId))
            {
                try
                {
                    Guid currentTeacherId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    GetSubjectsForTeacherCommand command = new GetSubjectsForTeacherCommand { UserId = currentTeacherId };
                    var allTaughtSubjects = await mediator.Send(command);
                    var taughtSubjects = allTaughtSubjects.Where(x => x.Subject.Id == new Guid(subjectId));
                    return new JsonResult(taughtSubjects);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }

        /// <summary>
        /// This is used to get the types for taught subject by teacher id and subject id.
        /// It's used by Admin
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> OnGetReturnTaughtSubjectTypes(string teacherId, string subjectId)
        {
            if (!string.IsNullOrEmpty(subjectId) && !string.IsNullOrEmpty(teacherId))
            {
                try
                {
                    var command = new GetTaughtSubjectTypesByTeacherAndSubjectCommand
                    {
                        SubjectId = new Guid(subjectId),
                        TeacherId = new Guid(teacherId)
                    };
                    var taughtSubjectTypes = await mediator.Send(command);
                    return new JsonResult(taughtSubjectTypes);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        } 
    }
}
