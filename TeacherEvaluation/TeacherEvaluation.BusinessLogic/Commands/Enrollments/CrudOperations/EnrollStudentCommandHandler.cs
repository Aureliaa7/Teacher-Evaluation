using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class EnrollStudentCommandHandler : AsyncRequestHandler<EnrollStudentCommand>
    {
        private readonly ITaughtSubjectRepository taughtSubjectRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IRepository<Enrollment> enrollmentRepository;

        public EnrollStudentCommandHandler(ITaughtSubjectRepository taughtSubjectRepository,
            IStudentRepository studentRepository, IRepository<Enrollment> enrollmentRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
            this.studentRepository = studentRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        protected override async Task Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await taughtSubjectRepository.Exists(x => x.Teacher.Id == request.TeacherId);
            bool subjectExists = await taughtSubjectRepository.Exists(x => x.Subject.Id == request.SubjectId);
            bool studentExists = await studentRepository.Exists(x => x.Id == request.StudentId);

            if (teacherExists && subjectExists && studentExists)
            {
                TaughtSubject taughtSubject = await taughtSubjectRepository.GetTaughtSubject(request.TeacherId, request.SubjectId, request.Type);
                Student student = await studentRepository.GetStudent(request.StudentId);
                Grade grade = new Grade
                {
                    Value = null,
                    Date = null,
                    Type = request.Type
                };

                Enrollment newEnrollment = new Enrollment()
                {
                    Student = student,
                    TaughtSubject = taughtSubject,
                    Grade = grade,
                    State = EnrollmentState.InProgress
                };
                await enrollmentRepository.Add(newEnrollment);
            }
            else
            {
                throw new ItemNotFoundException("The teacher or the subject was not found...");
            }
        }
    }
}
