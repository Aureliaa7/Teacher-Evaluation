using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Convertors;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    public class ViewByCriteriaModel : EnrollmentBaseModel
    {
        public bool DisplayTable { get; private set; }

        [BindProperty]
        [EnumDataType(typeof(EnrollmentState))]
        [Required(ErrorMessage = "The enrollment state is required")]
        public EnrollmentState? EnrollmentState { get; set; }

        public List<SelectListItem> EnrollmentStates = new List<SelectListItem>();

        public ViewByCriteriaModel(IMediator mediator): base(mediator)
        {
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
            if (ModelIsValid())
            {
                DisplayTable = true;
                GetEnrollmentsByCriteriaCommand command = new GetEnrollmentsByCriteriaCommand
                {
                    EnrollmentState = (EnrollmentState)EnrollmentState,
                    StudyDomainId = (Guid)StudyDomainId,
                    StudyYear = (int)StudyYear,
                    SpecializationId = (Guid)SpecializationId,
                    StudyProgramme = (StudyProgramme)StudyProgramme,
                    TaughtSubjectType = (TaughtSubjectType)Type
                };
                Enrollments = await mediator.Send(command);
            }
        }

        private bool ModelIsValid()
        {
            return (EnrollmentState != null && StudyDomainId != null && StudyYear != null && 
                SpecializationId != null && StudyProgramme != null && Type != null);
        }
    }
}
