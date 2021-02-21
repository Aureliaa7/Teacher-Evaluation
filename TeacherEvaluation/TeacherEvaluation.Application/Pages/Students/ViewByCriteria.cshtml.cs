using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Administrator")]
    public class ViewByCriteriaModel : StudentBaseModel
    {
        public bool TableIsVisible;

        public ViewByCriteriaModel(IMediator mediator): base(mediator)
        {
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (ModelIsValid())
            {
                TableIsVisible = true;
                GetStudentsByCriteriaCommand command = new GetStudentsByCriteriaCommand
                {
                    SpecializationId = (Guid)SpecializationId,
                    StudyDomainId = (Guid)StudyDomainId,
                    StudyProgramme = (StudyProgramme)StudyProgramme,
                    StudyYear = (int)StudyYear
                };
                CurrentRole.IsAdmin = true;
                Students = await mediator.Send(command);
            }
        }

        private bool ModelIsValid()
        {
            return (SpecializationId != null && StudyDomainId != null && StudyProgramme != null && StudyYear != null);
        }
    }
}
