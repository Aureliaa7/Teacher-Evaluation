using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectByIdCommandHandler : IRequestHandler<GetTaughtSubjectByIdCommand, TaughtSubject>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTaughtSubjectByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TaughtSubject> Handle(GetTaughtSubjectByIdCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await unitOfWork.TaughtSubjectRepository.Exists(x => x.Id == request.Id);
            if (taughtSubjectExists)
            {
                return await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectAsync(request.Id);
            }
            throw new ItemNotFoundException("The item was not found...");
        }
    }
}
