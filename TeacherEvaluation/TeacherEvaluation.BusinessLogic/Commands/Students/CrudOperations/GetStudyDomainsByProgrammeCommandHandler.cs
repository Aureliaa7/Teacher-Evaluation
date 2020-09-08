using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudyDomainsByProgrammeCommandHandler : IRequestHandler<GetStudyDomainsByProgrammeCommand, IEnumerable<StudyDomain>>
    {
        private readonly IRepository<StudyDomain> domainRepository;

        public GetStudyDomainsByProgrammeCommandHandler(IRepository<StudyDomain> domainRepository)
        {
            this.domainRepository = domainRepository;
        }
        public async Task<IEnumerable<StudyDomain>> Handle(GetStudyDomainsByProgrammeCommand request, CancellationToken cancellationToken)
        {
            var allStudyDomains = await domainRepository.GetAll();
            return allStudyDomains.Where(x => x.StudyProgramme == request.StudyProgramme);
        }
    }
}
