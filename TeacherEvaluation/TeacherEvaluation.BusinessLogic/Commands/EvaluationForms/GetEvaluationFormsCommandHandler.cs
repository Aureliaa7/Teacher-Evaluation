using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetEvaluationFormsCommandHandler : IRequestHandler<GetEvaluationFormsCommand, IEnumerable<Form>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetEvaluationFormsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Form>> Handle(GetEvaluationFormsCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.FormRepository.GetAllAsync();
        }
    }
}
