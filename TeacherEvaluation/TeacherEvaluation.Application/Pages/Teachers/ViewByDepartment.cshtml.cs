using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class ViewByDepartmentModel : PageModel
    {
        private readonly IMediator mediator;
        public bool TableIsVisible;

        [BindProperty]
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        [BindProperty]
        public IEnumerable<Teacher> Teachers { get; set; }

        public ViewByDepartmentModel(IMediator mediator)
        {
            this.mediator = mediator;
            Teachers = new List<Teacher>();
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
