using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject
{
    public class GetStudentsForSubjectCommandHandler : IRequestHandler<GetStudentsForSubjectCommand, IEnumerable<Student>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetStudentsForSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsForSubjectCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Id == request.TaughtSubjectId);
            if (taughtSubjectExists)
            {
                var enrollments = await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubject(request.TaughtSubjectId);
                return enrollments.Select(x => x.Student).ToList();
            }
            throw new ItemNotFoundException("The subject assignment was not found...");
        }
    }
}
