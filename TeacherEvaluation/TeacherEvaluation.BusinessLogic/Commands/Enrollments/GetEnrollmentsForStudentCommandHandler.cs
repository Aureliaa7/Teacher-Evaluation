using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments
{
    public class GetEnrollmentsForStudentCommandHandler : IRequestHandler<GetEnrollmentsForStudentCommand, IEnumerable<Enrollment>>
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<Grade> gradeRepository;

        public GetEnrollmentsForStudentCommandHandler(IEnrollmentRepository enrollmentRepository, 
            IRepository<Student> studentRepository, IRepository<Grade> gradeRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
        }

        public async Task<IEnumerable<Enrollment>> Handle(GetEnrollmentsForStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await studentRepository.Exists(x => x.Id == request.Id);
            if (studentExists)
            {
                return await enrollmentRepository.GetForStudent(request.Id);
            }
            throw new ItemNotFoundException("The student was not found...");
        }
    }
}
