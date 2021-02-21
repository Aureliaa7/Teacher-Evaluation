using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles= "Administrator")]
    public class ViewByCriteriaModel : TaughtSubjectBaseModel
    {
        public bool TableIsVisible;

        public ViewByCriteriaModel(IMediator mediator) : base(mediator)
        {
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (ModelIsValid())
            {
                GetTaughtSubjectsByCriteriaCommand command = new GetTaughtSubjectsByCriteriaCommand
                {
                    Department = (Department)Department,
                    TaughtSubjectType = (TaughtSubjectType)Type
                };
                TaughtSubjects = await mediator.Send(command);
                TableIsVisible = true;
                CurrentRole.IsAdmin = true;
            }
        }

        private bool ModelIsValid()
        {
            return (Department != null && Type != null);
        }
    }
}
