using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Teachers;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<Teacher> Teachers { get; set; }

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            Teachers = new List<Teacher>();
        }

        public async Task OnGetAsync()
        {
            GetAllTeachersCommand command = new GetAllTeachersCommand();
            Teachers = await mediator.Send(command);
        }
    }
}
