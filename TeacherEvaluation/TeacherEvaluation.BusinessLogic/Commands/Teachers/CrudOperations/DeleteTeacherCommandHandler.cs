using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class DeleteTeacherCommandHandler : AsyncRequestHandler<DeleteTeacherCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTeacherCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(x => x.Id == request.Id);
            if (teacherExists)
            {
                await unitOfWork.TeacherRepository.Delete(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The teacher was not found...");
            }
        }
    }
}
