using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
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
            bool availableForm = await unitOfWork.FormRepository.Exists(x => x.StartDate <= currentDate && x.EndDate > currentDate);
            if (availableForm)
            {
                return await unitOfWork.FormRepository.GetByDate(currentDate);
            }
            throw new NoEvaluationFormException("No form available for now");
        }
    }
}
