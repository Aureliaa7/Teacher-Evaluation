using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class GetEvaluationFormCommandHandler : IRequestHandler<GetEvaluationFormCommand, Form>
    {
        private readonly IFormRepository formRepository;

        public GetEvaluationFormCommandHandler(IFormRepository formRepository)
        {
            this.formRepository = formRepository;
        }

        public async Task<Form> Handle(GetEvaluationFormCommand request, CancellationToken cancellationToken)
        {
            var currentDate = DateTime.Now;
            bool availableForm = await formRepository.Exists(x => x.StartDate <= currentDate && x.EndDate > currentDate);
            if (availableForm)
            {
                return await formRepository.GetByDate(currentDate);
            }
            throw new NoEvaluationFormException("No form available for now");
        }
    }
}
