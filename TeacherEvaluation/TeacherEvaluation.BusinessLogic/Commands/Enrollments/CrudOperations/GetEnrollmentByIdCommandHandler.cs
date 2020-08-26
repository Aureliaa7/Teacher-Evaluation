using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetEnrollmentByIdCommandHandler : IRequestHandler<GetEnrollmentByIdCommand, Enrollment>
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrollmentByIdCommandHandler(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<Enrollment> Handle(GetEnrollmentByIdCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentExists = await enrollmentRepository.Exists(x => x.Id == request.Id);
            if (enrollmentExists)
            {
                return await enrollmentRepository.GetEnrollment(request.Id);
            }
            throw new ItemNotFoundException("The enrollment was not found...");
        }
    }
}
