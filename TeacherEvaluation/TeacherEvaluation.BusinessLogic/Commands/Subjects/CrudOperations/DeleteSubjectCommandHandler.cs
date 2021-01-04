using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class DeleteSubjectCommandHandler : AsyncRequestHandler<DeleteSubjectCommand>
    {

        private readonly IUnitOfWork unitOfWork;

        public DeleteSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await unitOfWork.SubjectRepository.Exists(x => x.Id == request.Id);
            if(subjectExists)
            {
                await unitOfWork.SubjectRepository.Remove(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The subject to be deleted was not found...");
            }
        }
    }
}
