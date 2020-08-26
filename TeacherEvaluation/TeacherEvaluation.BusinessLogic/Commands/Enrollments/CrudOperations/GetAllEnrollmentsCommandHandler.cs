using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetAllEnrollmentsCommandHandler : IRequestHandler<GetAllEnrollmentsCommand, IEnumerable<Enrollment>>
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetAllEnrollmentsCommandHandler(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Enrollment>> Handle(GetAllEnrollmentsCommand request, CancellationToken cancellationToken)
        {
            return await enrollmentRepository.GetAllWithRelatedEntities();
        }
    }
}
