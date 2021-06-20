using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic;
using TeacherEvaluation.BusinessLogic.Commands.Responses;

namespace TeacherEvaluation.Application.Pages.AllResponses.Responses
{
    public class ViewResponsesBaseModel : ResponseSearchDataModel
    {
        public ViewResponsesBaseModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task<JsonResult> OnGetRetrieveResponses(string teacherId, string formId, string taughtSubjectId)
        {
            if (!string.IsNullOrEmpty(teacherId) &&
                !string.IsNullOrEmpty(formId) &&
                !string.IsNullOrEmpty(taughtSubjectId))
            {
                ResponsesCommand command = new ResponsesCommand
                {
                    ResponseRetrievalCriteria = new EvaluationFormResponseRetrievalCriteria
                    {
                        FormId = new Guid(formId),
                        TeacherId = new Guid(teacherId),
                        TaughtSubjectId = taughtSubjectId
                    }
                };

                var responsesInfo = await mediator.Send(command);
                return new JsonResult(responsesInfo);
            }
            return new JsonResult("");
        }
    }
}
