using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class CheckEnrollmentAvailabilityCommandHandler : IRequestHandler<CheckEnrollmentAvailabilityCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public CheckEnrollmentAvailabilityCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckEnrollmentAvailabilityCommand request, CancellationToken cancellationToken)
        {
            bool enrollmentIsAvailable = false;

            bool enrollmentExists = await unitOfWork.EnrollmentRepository.Exists(x => x.TaughtSubject.Teacher.Id == request.TeacherId &&
                                                            x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                            x.TaughtSubject.Type == request.Type &&
                                                            x.Student.Id == request.StudentId);

            bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.Exists(x => x.Subject.Id == request.SubjectId && 
                                                                            x.Teacher.Id == request.TeacherId && x.Type == request.Type);
            if(taughtSubjectExists && !enrollmentExists)
            {
                enrollmentIsAvailable = true;
            }
            return enrollmentIsAvailable;
        }
    }
}
