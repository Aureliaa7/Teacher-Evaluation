using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.BusinessLogic.Commands.TagClouds;

namespace TeacherEvaluation.Application.Pages.AllResponses.Charts
{
    public class ViewChartsBaseModel : ResponseSearchDataModel
    {
        public ViewChartsBaseModel(IMediator mediator) : base(mediator)
        {
        }

        public JsonResult OnGetRetrieveResponses(string teacherId, string formId, string taughtSubjectId)
        {
            if (!string.IsNullOrEmpty(teacherId) &&
                !string.IsNullOrEmpty(formId) &&
                !string.IsNullOrEmpty(taughtSubjectId))
            {
                ChartsDataCommand command = new ChartsDataCommand
                {
                    FormId = new Guid(formId),
                    TeacherId = new Guid(teacherId),
                    TaughtSubjectId = taughtSubjectId
                };

                var questionsAndResponses = mediator.Send(command).Result;
                return new JsonResult(questionsAndResponses);
            }
            return new JsonResult("");
        }

        public JsonResult OnGetRetrieveTagCloud(string teacherId, string formId)
        {
            TagCloudCommand tagCloudCommand = new TagCloudCommand
            {
                FormId = new Guid(formId),
                TeacherId = new Guid(teacherId)
            };
            var tagClouds = mediator.Send(tagCloudCommand).Result;
            return new JsonResult(tagClouds);
        }
    }
}
