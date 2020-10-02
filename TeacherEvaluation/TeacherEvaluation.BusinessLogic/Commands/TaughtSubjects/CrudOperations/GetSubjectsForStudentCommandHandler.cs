using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetSubjectsForStudentCommandHandler : IRequestHandler<GetSubjectsForStudentCommand, IEnumerable<TaughtSubject>>
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetSubjectsForStudentCommandHandler(IRepository<ApplicationUser> userRepository, 
            IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository)
        {
            this.userRepository = userRepository;
            this.studentRepository = studentRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetSubjectsForStudentCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);
            if(userExists)
            {
                var students = await studentRepository.GetAllWithRelatedEntities();
                var studentId = students.Where(x => x.User.Id == request.UserId).Select(x => x.Id).First();
                var enrollments = await enrollmentRepository.GetForStudent(studentId);
                var taughtSubjects = enrollments.Where(x => x.TaughtSubject.Type == request.SubjectType && x.State == request.EnrollmentState)
                                                .Select(x => x.TaughtSubject);
                
                return taughtSubjects;
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
