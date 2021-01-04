using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetAllEnrollmentsCommandHandler : IRequestHandler<GetAllEnrollmentsCommand, IEnumerable<Enrollment>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllEnrollmentsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Enrollment>> Handle(GetAllEnrollmentsCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.EnrollmentRepository.GetAllWithRelatedEntities();
        }
    }
}
