using MediatR;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances
{
    public class AddAttendanceCommandHandler : AsyncRequestHandler<AddAttendanceCommand>
    {
        private readonly IRepository<Attendance> attendanceRepository;
        private readonly ITaughtSubjectRepository taughtSubjectRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public AddAttendanceCommandHandler(IRepository<Attendance> attendanceRepository, 
            ITaughtSubjectRepository taughtSubjectRepository, ITeacherRepository teacherRepository, 
            IRepository<ApplicationUser> userRepository, IEnrollmentRepository enrollmentRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
            this.teacherRepository = teacherRepository;
            this.userRepository = userRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        protected override async Task Handle(AddAttendanceCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);

            if(userExists)
            {
                var teacher = (await teacherRepository.GetAllWithRelatedEntities())
                              .Where(x => x.User.Id == request.UserId)
                              .First();
                bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id);

                if(taughtSubjectExists)
                {
                    var taughtSubject = (await taughtSubjectRepository.GetAllWithRelatedEntities())
                                        .Where(x => x.Subject.Id == request.SubjectId && x.Teacher.Id == teacher.Id && x.Type == request.Type)
                                        .First();

                    var enrollmentsForTaughtSubject = await enrollmentRepository.GetEnrollmentsForTaughtSubject(taughtSubject.Id);
                    var searchedEnrollment = enrollmentsForTaughtSubject.Where(x => x.Student.Id == request.StudentId)
                                                                        .First();
                    Attendance newAttendance = new Attendance
                    {
                        Id = Guid.NewGuid(),
                        Type = request.Type,
                        DateTime = request.DateTime,
                        Enrollment = searchedEnrollment
                    };
                    await attendanceRepository.Add(newAttendance);
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
