using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForEnrollmentsCommandHandler : IRequestHandler<GetSubjectsForEnrollmentsCommand, IEnumerable<Subject>>
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetSubjectsForEnrollmentsCommandHandler(IRepository<ApplicationUser> userRepository, IStudentRepository studentRepository,
           IEnrollmentRepository enrollmentRepository)
        {
            this.userRepository = userRepository;
            this.studentRepository = studentRepository;
            this.enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Subject>> Handle(GetSubjectsForEnrollmentsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await userRepository.Exists(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await studentRepository.GetByUserId(request.UserId);
                var allEnrollments = await enrollmentRepository.GetForStudent(student.Id);
                var enrollments = allEnrollments.Where(x => x.State == request.EnrollmentState);

                return enrollments.Select(x => x.TaughtSubject.Subject);
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
