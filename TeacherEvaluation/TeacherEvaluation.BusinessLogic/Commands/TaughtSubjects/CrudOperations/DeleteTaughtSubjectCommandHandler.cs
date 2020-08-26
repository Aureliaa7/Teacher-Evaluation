using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class DeleteTaughtSubjectCommandHandler : AsyncRequestHandler<DeleteTaughtSubjectCommand>
    {
        private readonly IRepository<TaughtSubject> taughtSubjectRepository;

        public DeleteTaughtSubjectCommandHandler(IRepository<TaughtSubject> taughtSubjectRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        protected override async Task Handle(DeleteTaughtSubjectCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Id == request.Id);
            if (taughtSubjectExists)
            {
                await taughtSubjectRepository.Remove(request.Id);
            }
            else
            {
                throw new ItemNotFoundException("The item was not found...");
            }
        }
    }
}
