using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class CheckEnrollmentAvailabilityCommandHandler : IRequestHandler<CheckEnrollmentAvailabilityCommand, bool>
    {
        private readonly IRepository<Enrollment> enrollmentRepository;
        private readonly IRepository<TaughtSubject> taughtSubjectRepository;

        public CheckEnrollmentAvailabilityCommandHandler(IRepository<Enrollment> enrollmentRepository, IRepository<TaughtSubject> taughtSubjectRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<bool> Handle(CheckEnrollmentAvailabilityCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentIsAvailable = false;

            bool enrollmentExists = await enrollmentRepository.Exists(x => x.TaughtSubject.Teacher.Id == request.TeacherId &&
                                                            x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                            x.TaughtSubject.Type == request.Type &&
                                                            x.Student.Id == request.StudentId);

            bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Subject.Id == request.SubjectId && 
                                                                            x.Teacher.Id == request.TeacherId && x.Type == request.Type);
            if(taughtSubjectExists && !enrollmentExists)
            {
                enrollmentIsAvailable = true;
            }
            return enrollmentIsAvailable;
        }
    }
}
