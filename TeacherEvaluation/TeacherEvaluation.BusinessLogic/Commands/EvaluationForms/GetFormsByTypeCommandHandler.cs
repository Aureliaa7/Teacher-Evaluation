using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetFormsByTypeCommandHandler : IRequestHandler<GetFormsByTypeCommand, IEnumerable<Form>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetFormsByTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Form>> Handle(GetFormsByTypeCommand request, CancellationToken cancellationToken)
        {
            return await unitOfWork.FormRepository.GetAllByType(request.Type);
        }
    }
}
