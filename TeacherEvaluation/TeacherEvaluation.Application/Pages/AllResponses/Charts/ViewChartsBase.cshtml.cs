using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sparc.TagCloud;
using System;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.BusinessLogic.Commands.TagClouds;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.AllResponses.Charts
{
    public class ViewChartsBaseModel : ResponseSearchDataModel
    {
        public ViewChartsBaseModel(IMediator mediator) : base(mediator)
        {
        }

        public JsonResult OnGetRetrieveResponses(string teacherId, string formId, string taughtSubjectId)
        {
            IDictionary<string, IDictionary<string, int>> questionsAndResponses = new Dictionary<string, IDictionary<string, int>>();
            if (!string.IsNullOrEmpty(teacherId) &&
                !string.IsNullOrEmpty(formId) &&
                !string.IsNullOrEmpty(taughtSubjectId))
            {
                try
                {
                    ChartsDataCommand command = new ChartsDataCommand
                    {
                        FormId = new Guid(formId),
                        TeacherId = new Guid(teacherId),
                        TaughtSubjectId = taughtSubjectId
                    };

                    questionsAndResponses = mediator.Send(command).Result;
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult(questionsAndResponses);
        }

        public JsonResult OnGetRetrieveTagCloud(string teacherId, string formId, string taughtSubjectId)
        {
            IEnumerable<TagCloudTag> tags = new List<TagCloudTag>();
            if(!string.IsNullOrEmpty(teacherId) && 
                !string.IsNullOrEmpty(formId) && 
                !string.IsNullOrEmpty(taughtSubjectId))
            {
                try
                {
                    TagCloudCommand tagCloudCommand = new TagCloudCommand
                    {
                        FormId = new Guid(formId),
                        TeacherId = new Guid(teacherId),
                        TaughtSubjectId = taughtSubjectId
                    };
                    tags = mediator.Send(tagCloudCommand).Result;
                }
                catch(ItemNotFoundException)
                {
                    // just go on
                }
            }
            
            return new JsonResult(tags);
        }
    }
}
