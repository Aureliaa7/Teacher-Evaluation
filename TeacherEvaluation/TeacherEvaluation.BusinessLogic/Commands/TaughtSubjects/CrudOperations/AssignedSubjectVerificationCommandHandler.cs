using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class AssignedSubjectVerificationCommandHandler : IRequestHandler<AssignedSubjectVerificationCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public AssignedSubjectVerificationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AssignedSubjectVerificationCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Teacher.Id == request.TeacherId &&
                                                             x.Subject.Id == request.SubjectId &&
                                                             x.Type == request.Type);
        }
    }
}
