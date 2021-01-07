using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.Application.Convertors;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    public class ViewByCriteriaModel : PageModel
    {
        private readonly IMediator mediator;

        public bool DisplayTable { get; private set; }

        [BindProperty]
        [EnumDataType(typeof(EnrollmentState))]
        [Required(ErrorMessage = "The enrollment state is required")]
        public EnrollmentState EnrollmentState { get; set; }

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "The study programme is required")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        [Required(ErrorMessage = "The subject type is required")]
        public TaughtSubjectType TaughtSubjectType { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The study domain is required")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        [Required(ErrorMessage = "The specialization is required")]
        public Guid SpecializationId { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public List<SelectListItem> EnrollmentStates = new List<SelectListItem>();

        public ViewByCriteriaModel(IMediator mediator)
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
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                DisplayTable = true;
                GetEnrollmentsByCriteriaCommand command = new GetEnrollmentsByCriteriaCommand
                {
                    EnrollmentState = EnrollmentState,
                    StudyDomainId = StudyDomainId,
                    StudyYear = (int)StudyYear,
                    SpecializationId = SpecializationId,
                    StudyProgramme = StudyProgramme,
                    TaughtSubjectType = TaughtSubjectType
                };
                Enrollments = await mediator.Send(command);
            }
        }
    }
}
