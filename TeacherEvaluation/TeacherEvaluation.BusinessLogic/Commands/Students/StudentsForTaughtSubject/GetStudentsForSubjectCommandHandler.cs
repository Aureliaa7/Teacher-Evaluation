using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject
{
    public class GetStudentsForSubjectCommandHandler : IRequestHandler<GetStudentsForSubjectCommand, IEnumerable<Student>>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<TaughtSubject> taughtSubjectRepository;

        public GetStudentsForSubjectCommandHandler(IEnrollmentRepository enrollmentRepository, IRepository<TaughtSubject> taughtSubjectRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<IEnumerable<Student>> Handle(GetStudentsForSubjectCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Id == request.TaughtSubjectId);
            if (taughtSubjectExists)
            {
                var enrollments = await enrollmentRepository.GetEnrollmentsForTaughtSubject(request.TaughtSubjectId);
                return enrollments.Select(x => x.Student).ToList();
            }
            throw new ItemNotFoundException("The subject assignment was not found...");
        }
    }
}
