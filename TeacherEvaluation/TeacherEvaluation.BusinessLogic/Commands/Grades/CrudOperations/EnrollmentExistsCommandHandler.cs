using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class EnrollmentExistsCommandHandler : IRequestHandler<EnrollmentExistsCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public EnrollmentExistsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(EnrollmentExistsCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.ExistsAsync(x => x.Id == request.StudentId);
            bool subjectExists = await unitOfWork.SubjectRepository.ExistsAsync(x => x.Id == request.SubjectId);
            if (studentExists && subjectExists)
            {
                return await unitOfWork.EnrollmentRepository.ExistsAsync(x => x.TaughtSubject.Subject.Id == request.SubjectId &&
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
