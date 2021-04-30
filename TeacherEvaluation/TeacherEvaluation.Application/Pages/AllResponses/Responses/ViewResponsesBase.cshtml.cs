using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using TeacherEvaluation.BusinessLogic.Commands.Responses;

namespace TeacherEvaluation.Application.Pages.AllResponses.Responses
{
    public class ViewResponsesBaseModel : ResponseSearchDataModel
    {
        public ViewResponsesBaseModel(IMediator mediator) : base(mediator)
        {
        }

        public JsonResult OnGetRetrieveResponses(string teacherId, string formId, string taughtSubjectId)
        {
            if (!string.IsNullOrEmpty(teacherId) &&
                !string.IsNullOrEmpty(formId) &&
                !string.IsNullOrEmpty(taughtSubjectId))
            {
                ResponsesCommand command = new ResponsesCommand
                {
                    FormId = new Guid(formId),
                    TeacherId = new Guid(teacherId),
                    TaughtSubjectId = taughtSubjectId
                };

                var responsesInfo = mediator.Send(command).Result;
                return new JsonResult(responsesInfo);
            }
            return new JsonResult("");
        }
    }
}
