using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetAllTaughtSubjectsCommandHandler : IRequestHandler<GetAllTaughtSubjectsCommand, IEnumerable<TaughtSubject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllTaughtSubjectsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetAllTaughtSubjectsCommand request, CancellationToken cancellationToken)
        {
            var taughtSubjects = await unitOfWork.TaughtSubjectRepository.GetAllWithRelatedEntitiesAsync();
            return taughtSubjects;
        }
    }
}
