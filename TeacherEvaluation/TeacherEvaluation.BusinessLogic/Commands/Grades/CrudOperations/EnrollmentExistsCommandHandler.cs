using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class EnrollmentExistsCommandHandler : IRequestHandler<EnrollmentExistsCommand, bool>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Subject> subjectRepository;

        public EnrollmentExistsCommandHandler(IEnrollmentRepository enrollmentRepository, 
            IRepository<Student> studentRepository, IRepository<Subject> subjectRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.studentRepository = studentRepository;
            this.subjectRepository = subjectRepository;
        }

        public async Task<bool> Handle(EnrollmentExistsCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.StudentId);
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.SubjectId);
            if (studentExists && subjectExists)
            {
                return await enrollmentRepository.Exists(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
                                                    x.Student.Id == request.StudentId && 
                                                    x.TaughtSubject.Type == request.Type);
            }
            else
            {
                throw new ItemNotFoundException("The student or the subject was not found...");
            }
        }
    }
}
