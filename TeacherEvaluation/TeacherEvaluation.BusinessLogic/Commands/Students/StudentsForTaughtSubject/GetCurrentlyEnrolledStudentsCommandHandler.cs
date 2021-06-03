using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject
{
    public class GetCurrentlyEnrolledStudentsCommandHandler : IRequestHandler<GetCurrentlyEnrolledStudentsCommand, IEnumerable<Student>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetCurrentlyEnrolledStudentsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> Handle(GetCurrentlyEnrolledStudentsCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Id == request.TaughtSubjectId);
            if (taughtSubjectExists)
            {
                var enrollments = (await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubject(request.TaughtSubjectId))
                                    .Where(e => e.State == EnrollmentState.InProgress);
                return enrollments.Select(x => x.Student);
            }
            throw new ItemNotFoundException("The subject assignment was not found...");
        }
    }
}
