using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetEvaluationFormCommandHandler : IRequestHandler<GetEvaluationFormCommand, Form>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetEvaluationFormCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Form> Handle(GetEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            var currentDate = DateTime.Now;
            bool isFormAvailable = await unitOfWork.FormRepository.ExistsAsync(x => x.StartDate <= currentDate && x.EndDate > currentDate);
            if (isFormAvailable)
            {
                return await unitOfWork.FormRepository.GetByDateAsync(currentDate);
            }
            throw new NoEvaluationFormException("No form available for now");
        }
    }
}
