using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForInProgressEnrollmentsCommandHandler : 
        IRequestHandler<GetSubjectsForInProgressEnrollmentsCommand, IEnumerable<Subject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForInProgressEnrollmentsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> Handle(GetSubjectsForInProgressEnrollmentsCommand request,
            CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(x => x.Id == request.UserId);
            if (userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserIdAsync(request.UserId);
                var allEnrollments = await unitOfWork.EnrollmentRepository.GetForStudentAsync(student.Id);
                var enrollments = allEnrollments.Where(x => x.State == EnrollmentState.InProgress);

                var subjectEnrollments = enrollments.GroupBy(x => x.TaughtSubject.Subject.Name).Select(x => x.First()).ToList();
                return subjectEnrollments.Select(x => x.TaughtSubject.Subject).ToList();
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
