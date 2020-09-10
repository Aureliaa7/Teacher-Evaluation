using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetAttendancesForSubjectCommandHandler : IRequestHandler<GetAttendancesForSubjectCommand, IEnumerable<Attendance>>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IAttendanceRepository attendanceRepository;
        private readonly ITaughtSubjectRepository taughtSubjectRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IStudentRepository studentRepository;

        public GetAttendancesForSubjectCommandHandler(IEnrollmentRepository enrollmentRepository, IAttendanceRepository attendanceRepository,
            ITaughtSubjectRepository taughtSubjectRepository, IRepository<ApplicationUser> userRepository, IStudentRepository studentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.attendanceRepository = attendanceRepository;
            this.taughtSubjectRepository = taughtSubjectRepository;
            this.userRepository = userRepository;
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Attendance>> Handle(GetAttendancesForSubjectCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Id == request.TaughtSubjectId);
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);
            if (taughtSubjectExists && userExists)
            {
                var students = await studentRepository.GetAllWithRelatedEntities();
                var student = students.Where(x => x.User.Id == request.UserId).First();

                var enrollments = await enrollmentRepository.GetEnrollmentsForTaughtSubject(request.TaughtSubjectId);
                var enrollment = enrollments.Where(x => x.Student.Id == student.Id).First();
                return await attendanceRepository.GetAttendancesWithRelatedEntities(enrollment.Id);
            }
            throw new ItemNotFoundException("The subject or the user was not found...");
        }
    }
}
