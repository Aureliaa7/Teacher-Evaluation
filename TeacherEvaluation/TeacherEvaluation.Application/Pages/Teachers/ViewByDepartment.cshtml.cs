using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    [Authorize(Roles = "Administrator")]
    public class ViewByDepartmentModel : TeachersListModel
    {
        public bool TableIsVisible;

        [BindProperty]
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        public ViewByDepartmentModel(IMediator mediator) : base(mediator)
        {
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            GetTeachersByDepartmentCommand command = new GetTeachersByDepartmentCommand { Department = Department };
            Teachers = await mediator.Send(command);
            TableIsVisible = true;
        }
    }
}
