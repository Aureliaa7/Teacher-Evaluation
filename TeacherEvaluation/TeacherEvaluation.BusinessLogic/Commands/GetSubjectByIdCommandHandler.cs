using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class GetSubjectByIdCommandHandler : IRequestHandler<GetSubjectByIdCommand, Subject>
    {
        private readonly IRepository<Subject> subjectRepository;

        public GetSubjectByIdCommandHandler(IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public async Task<Subject> Handle(GetSubjectByIdCommand request, CancellationToken cancellationToken)
        {
            bool subjectExists = await subjectRepository.Exists(x => x.Id == request.Id);
            if (subjectExists)
            {
                Subject searchedSubject = await subjectRepository.Get(request.Id);
                return searchedSubject;
            }
            throw new ItemNotFoundException("The subject to be deleted was not found...");
        }
    }
}
