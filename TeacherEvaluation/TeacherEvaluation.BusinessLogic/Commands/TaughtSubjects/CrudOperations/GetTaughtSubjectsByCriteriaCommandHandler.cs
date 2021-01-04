using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectsByCriteriaCommandHandler : IRequestHandler<GetTaughtSubjectsByCriteriaCommand, IEnumerable<TaughtSubject>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTaughtSubjectsByCriteriaCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaughtSubject>> Handle(GetTaughtSubjectsByCriteriaCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteria(request.Department, request.TaughtSubjectType);
        }
    }
}
