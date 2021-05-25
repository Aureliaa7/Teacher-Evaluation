using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class DeleteEnrollmentCommandHandler : AsyncRequestHandler<DeleteEnrollmentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteEnrollmentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentExists = await unitOfWork.EnrollmentRepository.ExistsAsync(x => x.Id == request.Id);
            if (enrollmentExists)
            {
                await unitOfWork.EnrollmentRepository.RemoveAsync(request.Id);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The enrollment was not found...");
            }
        }
    }
}
