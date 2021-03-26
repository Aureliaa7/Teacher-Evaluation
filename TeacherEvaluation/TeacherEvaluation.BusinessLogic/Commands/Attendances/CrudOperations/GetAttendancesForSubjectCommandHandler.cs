using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetAttendancesForSubjectCommandHandler : IRequestHandler<GetAttendancesForSubjectCommand, int>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAttendancesForSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetAttendancesForSubjectCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.Exists(x => x.Id == request.TaughtSubjectId);
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserId);
            if (taughtSubjectExists && userExists)
            {
                var students = await unitOfWork.StudentRepository.GetAllWithRelatedEntities();
                var student = students.Where(x => x.User.Id == request.UserId).First();

                var enrollments = await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubject(request.TaughtSubjectId);
                var enrollment = enrollments.Where(x => x.Student.Id == student.Id).First();
                return enrollment.NumberOfAttendances;
            }
            throw new ItemNotFoundException("The subject or the user was not found...");
        }
    }
}