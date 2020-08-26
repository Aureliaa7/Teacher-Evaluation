using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class DeleteEnrollmentCommandHandler : AsyncRequestHandler<DeleteEnrollmentCommand>
    {
        private readonly IRepository<Enrollment> enrollmentRepository;

        public DeleteEnrollmentCommandHandler(IRepository<Enrollment> enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }

        protected override async Task Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentExists = await enrollmentRepository.Exists(x => x.Id == request.Id);
            if (enrollmentExists)
            {
                await enrollmentRepository.Remove(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The enrollment was not found...");
            }
        }
    }
}
