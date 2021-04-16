using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.BusinessLogic.Commands.TagClouds;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    public class ViewChartsBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        public Guid FormId { get; set; }

        [BindProperty]
        public string SelectedSubjectId { get; set; }

        public ViewChartsBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
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
