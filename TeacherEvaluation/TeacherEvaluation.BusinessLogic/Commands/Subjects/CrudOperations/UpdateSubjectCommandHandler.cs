using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class UpdateSubjectCommandHandler : AsyncRequestHandler<UpdateSubjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await unitOfWork.SubjectRepository.Exists(x => x.Id == request.Id);
            if (subjectExists)
            {
                Subject subjectToBeUpdated = await unitOfWork.SubjectRepository.Get(request.Id);
                subjectToBeUpdated.Name = request.Name;
                subjectToBeUpdated.NumberOfCredits = request.NumberOfCredits;
                await unitOfWork.SubjectRepository.Update(subjectToBeUpdated);
            }
            else
            {
                throw new ItemNotFoundException("The subject was not found...");
            }
        }
    }
}
