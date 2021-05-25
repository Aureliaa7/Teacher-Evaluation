using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForEnrollmentsCommandHandler : IRequestHandler<GetSubjectsForEnrollmentsCommand, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForEnrollmentsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> Handle(GetSubjectsForEnrollmentsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserId);
                var allEnrollments = await unitOfWork.EnrollmentRepository.GetForStudent(student.Id);
                var enrollments = allEnrollments.Where(x => x.State == request.EnrollmentState);

                var subjectEnrollments = enrollments.GroupBy(x => x.TaughtSubject.Subject.Name).Select(x => x.First()).ToList();
                return subjectEnrollments.Select(x => x.TaughtSubject.Subject).ToList();
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
