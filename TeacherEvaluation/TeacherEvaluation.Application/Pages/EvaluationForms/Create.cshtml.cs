using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using TeacherEvaluation.BusinessLogic;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public List<string> LikertQuestions { get; set; }

        [BindProperty]
        public List<string> FreeFormQuestions { get; set; }

        [BindProperty]
        public int NoFreeFormQuestions { get; set; } = Constants.NumberOfQuestionsWithTextAnswer;

        [BindProperty]
        public int NoLikertQuestions { get; set; } = Constants.NumberOfQuestionsWithAnswerOption;

        [BindProperty]
        [Required(ErrorMessage = "The minimum number of attendances is required")]
        [Range(1, 14, ErrorMessage = "The number of attendances must be between 1 and 14")]
        [Display(Name = "Min number of attendances")]
        public int? NumberOfAttendances { get; set; } = null;

        [BindProperty]
        [Required(ErrorMessage = "The start date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The end date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "End date")]
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
            if (ModelState.IsValid)
            {
                LikertQuestions.AddRange(FreeFormQuestions);

                CreateFormCommand command = new CreateFormCommand
                {
                    Questions = LikertQuestions,
                    MinNumberOfAttendances = (int)NumberOfAttendances,
                    StartDate = StartDate,
                    EndDate = EndDate

                };
                await mediator.Send(command);
                return RedirectToPage("/Dashboards/Dean");
            }
            else
            {
                return Page();
            }
        }
    }
}
