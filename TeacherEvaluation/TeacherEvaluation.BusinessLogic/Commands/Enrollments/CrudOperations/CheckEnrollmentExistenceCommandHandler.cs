using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class CheckEnrollmentExistenceCommandHandler : IRequestHandler<CheckEnrollmentExistenceCommand, bool>
    {
        private readonly IRepository<Enrollment> enrollmentRepository;

        public CheckEnrollmentExistenceCommandHandler(IRepository<Enrollment> enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<bool> Handle(CheckEnrollmentExistenceCommand request, CancellationToken cancellationToken)
        {
            return await enrollmentRepository.Exists(x => x.TaughtSubject.Teacher.Id == request.TeacherId &&
                                                            x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                            x.TaughtSubject.Type == request.Type &&
                                                            x.Student.Id == request.StudentId);
        }
    }
}
