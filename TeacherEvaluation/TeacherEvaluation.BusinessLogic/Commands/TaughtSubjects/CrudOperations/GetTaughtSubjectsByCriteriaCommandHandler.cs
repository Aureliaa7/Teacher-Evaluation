using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectsByCriteriaCommandHandler : IRequestHandler<GetTaughtSubjectsByCriteriaCommand, IEnumerable<TaughtSubject>>
    {
        private readonly ITaughtSubjectRepository taughtSubjectRepository;

        public GetTaughtSubjectsByCriteriaCommandHandler(ITaughtSubjectRepository taughtSubjectRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetTaughtSubjectsByCriteriaCommand request, CancellationToken cancellationToken)
        {
            return await taughtSubjectRepository.GetTaughtSubjectsByCriteria(request.Department, request.TaughtSubjectType);
        }
    }
}
