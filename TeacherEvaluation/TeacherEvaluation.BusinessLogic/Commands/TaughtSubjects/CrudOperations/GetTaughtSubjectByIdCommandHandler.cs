using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectByIdCommandHandler : IRequestHandler<GetTaughtSubjectByIdCommand, TaughtSubject>
    {
        private readonly ITaughtSubjectRepository taughtSubjectRepository;

        public GetTaughtSubjectByIdCommandHandler(ITaughtSubjectRepository taughtSubjectRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<TaughtSubject> Handle(GetTaughtSubjectByIdCommand request, CancellationToken cancellationToken)
        {
            bool taughtSubjectExists = await taughtSubjectRepository.Exists(x => x.Id == request.Id);
            if (taughtSubjectExists)
            {
                return await taughtSubjectRepository.GetTaughtSubject(request.Id);
            }
            throw new ItemNotFoundException("The item was not found...");
        }
    }
}
