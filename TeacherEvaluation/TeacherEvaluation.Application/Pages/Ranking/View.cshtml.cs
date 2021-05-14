using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Questions;
using TeacherEvaluation.BusinessLogic.Commands.Ranking;
using TeacherEvaluation.BusinessLogic.Enums;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.Ranking
{
    [Authorize(Roles = "Dean")]
    public class ViewModel : PageModel
    {
        private readonly IMediator mediator;
        private Guid formId;

        public ViewModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Guid QuestionId { get; set; }

        [BindProperty]
        public List<SelectListItem> LikertQuestions { get; set; } = new List<SelectListItem>();

        public async Task OnGetAsync(Guid formId)
        {
            this.formId = formId;
            var command = new LikertQuestionsCommand { FormId = formId } ;
            var likertQuestions = await mediator.Send(command);
            LikertQuestions = likertQuestions.Select(x =>
                                                new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.Text
                                                }).ToList();
        }

        public JsonResult OnGetReturnTopTeachers(string questionId, string rankingType)
        {
           IDictionary<string, long> teachersData = new Dictionary<string, long>();
            if (!string.IsNullOrEmpty(questionId) && !string.IsNullOrEmpty(rankingType))
            {
                RankingType type = (RankingType)Enum.Parse(typeof(RankingType), rankingType, true);
                RankingCommand command = new RankingCommand
                {
                    QuestionId = new Guid(questionId),
                    RankingType = type
                };
                var topTeachers = mediator.Send(command).Result;
                teachersData = GetTeachersInfo(topTeachers);
            }
            return new JsonResult(teachersData);
        }

        private IDictionary<string, long> GetTeachersInfo(IDictionary<TeacherVm, long> topTeachers)
        {
            IDictionary<string, long> teachersDetails = new Dictionary<string, long>();
            foreach(var teacher in topTeachers)
            {
                var teacherInfo = string.Concat(teacher.Key.Name, " ", teacher.Key.Department, " ",
                    teacher.Key.Degree);
                teachersDetails.Add(teacherInfo, teacher.Value);
            }
            return teachersDetails;
        }
    }
}