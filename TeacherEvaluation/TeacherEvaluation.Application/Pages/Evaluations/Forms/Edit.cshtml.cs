using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.Application.Convertors;
using TeacherEvaluation.BusinessLogic.Commands.EvaluationForms;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Evaluations.Forms
{
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
            EnrollmentStates = Enum.GetValues(typeof(EnrollmentState))
              .Cast<EnrollmentState>()
              .Select(x =>
              {
                  string displayText = EnrollmentStateConvertor.ToDisplayString(x);
                  return new SelectListItem(displayText, x.ToString());
              })
              .ToList();
        }

        [BindProperty]
        public Guid FormId { get; set; }

        [BindProperty]
        [EnumDataType(typeof(EnrollmentState))]
        public EnrollmentState EnrollmentState { get; set; }

        public List<SelectListItem> EnrollmentStates = new List<SelectListItem>();

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

        public void OnGet(Guid? id)
        {
            if(id != null)
            {
                FormId = (Guid)id;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            EditFormCommand command = new EditFormCommand
            {
                StartDate = StartDate,
                EndDate = EndDate,
                MinNumberAttendances = (int) NumberOfAttendances,
                FormId = FormId,
                EnrollmentState = EnrollmentState.InProgress
            };
            await mediator.Send(command);
            return RedirectToPage("/Dashboards/Dean");
        }
    }
}
