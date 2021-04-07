using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.BusinessLogic.Commands.TagClouds;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    public class ViewResponsesModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        public Guid FormId { get; set; }

        [BindProperty]
        public List<SelectListItem> Teachers { get; set; } = new List<SelectListItem>();

        public ViewResponsesModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet(Guid formId)
        {
            FormId = formId;
            try
            {
                GetAllTeachersCommand getTeachersCommand = new GetAllTeachersCommand();
                var teachers = mediator.Send(getTeachersCommand).Result;


                Teachers = teachers.Select(x =>
                                                new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.User.FirstName + " " + x.User.LastName
                                                }).ToList();
            }
            catch (ItemNotFoundException) { }
        }

        public JsonResult OnGetRetrieveResponses(string teacherId, string formId)
        {
            GetResponsesCommand command = new GetResponsesCommand 
            {
                FormId = new Guid(formId), 
                TeacherId = new Guid(teacherId) 
            };

            var questionsAndResponses = mediator.Send(command).Result;
            return new JsonResult(questionsAndResponses);
        }

        public JsonResult OnGetReturnTagCloud(string teacherId, string formId)
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
