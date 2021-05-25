using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetEnrollmentByIdCommandHandler : IRequestHandler<GetEnrollmentByIdCommand, Enrollment>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetEnrollmentByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Enrollment> Handle(GetEnrollmentByIdCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentExists = await unitOfWork.EnrollmentRepository.ExistsAsync(x => x.Id == request.Id);
            if (enrollmentExists)
            {
                return await unitOfWork.EnrollmentRepository.GetEnrollment(request.Id);
            }
            throw new ItemNotFoundException("The enrollment was not found...");
        }
    }
}
