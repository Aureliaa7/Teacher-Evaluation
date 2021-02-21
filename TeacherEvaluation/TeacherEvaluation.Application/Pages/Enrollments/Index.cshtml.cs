using MediatR;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    public class IndexModel : EnrollmentBaseModel
    {
        public IndexModel(IMediator mediator): base(mediator)
        {
        }

        public async Task OnGetAsync()
        {
            GetAllEnrollmentsCommand command = new GetAllEnrollmentsCommand();
            Enrollments = await mediator.Send(command);
        }
    }
}
