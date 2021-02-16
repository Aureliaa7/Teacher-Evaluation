using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetSubjectsForStudentCommandHandler : IRequestHandler<GetSubjectsForStudentCommand, IEnumerable<TaughtSubject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSubjectsForStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetSubjectsForStudentCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(x => x.Id == request.UserId);
            if(userExists)
            {
                var student = await unitOfWork.StudentRepository.GetByUserId(request.UserId);
                var enrollments = await unitOfWork.EnrollmentRepository.GetForStudent(student.Id);
                var taughtSubjects = enrollments.Where(x => x.TaughtSubject.Type == request.SubjectType && x.State == request.EnrollmentState)
                                                .Select(x => x.TaughtSubject);
                
                return taughtSubjects;
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
