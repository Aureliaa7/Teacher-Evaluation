using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetSpecializationsByDomainCommandHandler : IRequestHandler<GetSpecializationsByDomainCommand, IEnumerable<Specialization>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSpecializationsByDomainCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Specialization>> Handle(GetSpecializationsByDomainCommand request, CancellationToken cancellationToken)
        {
            bool domainExists = await unitOfWork.StudyDomainRepository.ExistsAsync(x => x.Id == request.StudyDomainId);
            if(domainExists)
            {
                var allSpecializations = await unitOfWork.SpecializationRepository.GetAllWithRelatedEntities();
                return allSpecializations.Where(x => x.StudyDomain.Id == request.StudyDomainId);
            }
            throw new ItemNotFoundException("The study domain was not found...");
        }
    }
}
