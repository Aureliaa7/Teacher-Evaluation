using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetAllTaughtSubjectsCommandHandler : IRequestHandler<GetAllTaughtSubjectsCommand, IEnumerable<TaughtSubject>>
    {
        private readonly ITaughtSubjectRepository taughtSubjectRepository;

        public GetAllTaughtSubjectsCommandHandler(ITaughtSubjectRepository taughtSubjectRepository)
        {
            this.taughtSubjectRepository = taughtSubjectRepository;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetAllTaughtSubjectsCommand request, CancellationToken cancellationToken)
        {
            return await taughtSubjectRepository.GetAllWithRelatedEntities();
        }
    }
}
