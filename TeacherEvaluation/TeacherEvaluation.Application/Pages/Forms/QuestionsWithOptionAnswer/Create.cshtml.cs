using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Forms.QuestionsWithOptionAnswer
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public List<string> Questions { get; set; }

        [BindProperty]
        [EnumDataType(typeof(EnrollmentState))]
        public EnrollmentState EnrollmentState { get; set; }

        [BindProperty]
        [Required(ErrorMessage ="The minimum number of attendances is required")]
        [Range(1, 14, ErrorMessage ="The number of attendances must be between 1 and 14")]
        public int? NumberOfAttendances { get; set; } = null;

        [BindProperty]
        [Required(ErrorMessage = "The start date is required")]
        public DateTime StartDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The end date is required")]
        public DateTime EndDate { get; set; }


        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CreateFormForQuestionWithOptionCommand command = new CreateFormForQuestionWithOptionCommand
            {
                MinNumberAttendances = (int)NumberOfAttendances,
                EnrollmentState = EnrollmentState,
                StartDate = StartDate,
                EndDate = EndDate, 
                Questions = Questions
            };
            await mediator.Send(command);
            return RedirectToPage("/Dashboards/Dean");
        }

        public void OnPostAddQuestion(string question)
        {
            Questions.Add(question);
        }
    }
}