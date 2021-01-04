using MediatR;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class AddAttendanceCommandHandler : AsyncRequestHandler<AddAttendanceCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddAttendanceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(AddAttendanceCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserId);

            if(userExists)
            {
                var teacher = (await unitOfWork.TeacherRepository.GetAllWithRelatedEntities())
                              .Where(x => x.User.Id == request.UserId)
                              .First();
                bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.Exists(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id);

                if(taughtSubjectExists)
                {
                    var taughtSubject = (await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntities())
                                        .Where(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id && x.Type == request.Type)
                                        .First();

                    var enrollmentsForTaughtSubject = await unitOfWork.EnrollmentRepository.GetEnrollmentsForTaughtSubject(taughtSubject.Id);
                    var searchedEnrollment = enrollmentsForTaughtSubject.Where(x => x.Student.Id == request.StudentId)
                                                                        .First();
                    Attendance newAttendance = new Attendance
                    {
                        Id = Guid.NewGuid(),
                        Type = request.Type,
                        DateTime = request.DateTime,
                        Enrollment = searchedEnrollment
                    };
                    await unitOfWork.AttendanceRepository.Add(newAttendance);
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
