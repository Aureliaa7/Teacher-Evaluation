using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class DeleteStudentCommandHandler : AsyncRequestHandler<DeleteStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            bool studentExists = await unitOfWork.StudentRepository.ExistsAsync(x => x.Id == request.Id);
            if (studentExists)
            {
                await unitOfWork.StudentRepository.DeleteAsync(request.Id);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ItemNotFoundException("The student was not found...");
            }         
        }
    }
}
