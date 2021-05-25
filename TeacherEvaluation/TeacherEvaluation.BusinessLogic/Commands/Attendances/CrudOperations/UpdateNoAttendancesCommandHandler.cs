using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class UpdateNoAttendancesCommandHandler : AsyncRequestHandler<UpdateNoAttendancesCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateNoAttendancesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(UpdateNoAttendancesCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);

            if (userExists)
            {
                var teacher = (await unitOfWork.TeacherRepository.GetAllWithRelatedEntitiesAsync())
                              .Where(x => x.User.Id == request.UserId)
                              .First();
                bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id);

                if (taughtSubjectExists)
                {
                    var taughtSubject = (await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntitiesAsync())
                                        .Where(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id && x.Type == request.Type)
                                        .First();

                    var enrollmentsForTaughtSubject = await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubject(taughtSubject.Id);
                    var searchedEnrollment = enrollmentsForTaughtSubject.Where(x => x.Student.Id == request.StudentId)
                                                                        .First();
                    searchedEnrollment.NumberOfAttendances = request.NumberOfAttendances;
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new ItemNotFoundException("The taught subject was not found...");
                }
            }
            else
            {
                throw new ItemNotFoundException("The user was not found...");
            }
        }
    }
}
