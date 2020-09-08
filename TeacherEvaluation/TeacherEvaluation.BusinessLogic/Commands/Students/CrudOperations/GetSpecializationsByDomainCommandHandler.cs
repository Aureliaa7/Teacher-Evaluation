using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetSpecializationsByDomainCommandHandler : IRequestHandler<GetSpecializationsByDomainCommand, IEnumerable<Specialization>>
    {
        private readonly ISpecializationRepository specializationRepository;
        private readonly IRepository<StudyDomain> domainRepository;

        public GetSpecializationsByDomainCommandHandler(ISpecializationRepository specializationRepository, IRepository<StudyDomain> domainRepository)
        {
            this.specializationRepository = specializationRepository;
            this.domainRepository = domainRepository;
        }

        public async Task<IEnumerable<Specialization>> Handle(GetSpecializationsByDomainCommand request, CancellationToken cancellationToken)
        {
            bool domainExists = await domainRepository.Exists(x => x.Id == request.StudyDomainId);
            if(domainExists)
            {
                var allSpecializations = await specializationRepository.GetAllWithRelatedEntities();
                return allSpecializations.Where(x => x.StudyDomain.Id == request.StudyDomainId);
            }
            throw new ItemNotFoundException("The study domain was not found...");
        }
    }
}
