using System;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using TeacherEvaluation.BusinessLogic;
using TeacherEvaluation.Application.Convertors;
using TeacherEvaluation.Domain.DomainEntities.Enums;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;

namespace TeacherEvaluation.Application.Pages.EvaluationForms
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public List<string> Questions { get; set; }

        [BindProperty]
        public int NumberOfQuestions { get; set; } = Constants.TotalNumberOfQuestions;

        [BindProperty]
        [EnumDataType(typeof(EnrollmentState))]
        public EnrollmentState EnrollmentState { get; set; }

        public IEnumerable<SelectListItem> EnrollmentStates = new List<SelectListItem>();

        [BindProperty]
        [Required(ErrorMessage = "The minimum number of attendances is required")]
        [Range(1, 14, ErrorMessage = "The number of attendances must be between 1 and 14")]
        public int? NumberOfAttendances { get; set; } = null;

        [BindProperty]
        [Required(ErrorMessage = "The start date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The end date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }


        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
            EnrollmentStates = GetEnrollmentStates();
        }

        private IEnumerable<SelectListItem> GetEnrollmentStates()
        {
            var enrollmentStates = Enum.GetValues(typeof(EnrollmentState))
              .Cast<EnrollmentState>()
              .Select(x =>
              {
                  string displayText = EnrollmentStateConvertor.ToDisplayString(x);
                  return new SelectListItem(displayText, x.ToString());
              })
              .ToList();

            return enrollmentStates;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CreateFormCommand command = new CreateFormCommand
                {
                    Questions = Questions,
                    MinNumberOfAttendances = (int)NumberOfAttendances,
                    EnrollmentState = EnrollmentState,
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
