using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class DeleteTaughtSubjectCommandHandler : AsyncRequestHandler<DeleteTaughtSubjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTaughtSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteTaughtSubjectCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.ExistsAsync(x => x.Id == request.Id);
            if (taughtSubjectExists)
            {
                await unitOfWork.TaughtSubjectRepository.RemoveAsync(request.Id);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The item was not found...");
            }
        }
    }
}
