using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class GetQuestionsForFormCommandHandler : IRequestHandler<GetQuestionsForFormCommand, IEnumerable<QuestionWithOptionAnswer>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetQuestionsForFormCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QuestionWithOptionAnswer>> Handle(GetQuestionsForFormCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.Exists(x => x.Id == request.FormId);
            if (formExists)
            {
                var questions = await unitOfWork.QuestionWithOptionAnswerRepository.GetQuestionsWithRelatedEntities(request.FormId);
                return questions;
            }
            throw new ItemNotFoundException("The form was not found...");
        }
    }
}
