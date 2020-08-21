using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Students;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<Student> Students { get; set; }

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            Students = new List<Student>();
        }

        public async Task OnGetAsync()
        {
            GetAllStudentsCommand command = new GetAllStudentsCommand();
            Students = await mediator.Send(command);
        }
    }
}
