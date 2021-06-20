using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudyDomainsByProgrammeCommandHandler : IRequestHandler<GetStudyDomainsByProgrammeCommand, IEnumerable<StudyDomain>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetStudyDomainsByProgrammeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<StudyDomain>> Handle(GetStudyDomainsByProgrammeCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.StudyDomainRepository.GetByStudyProgrammeAsync(request.StudyProgramme);
        }
    }
}
