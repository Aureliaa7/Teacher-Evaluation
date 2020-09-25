using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class GetQuestionsForFormCommandHandler : IRequestHandler<GetQuestionsForFormCommand, IEnumerable<QuestionWithOptionAnswer>>
    {
        private readonly IQuestionWithOptionAnswerRepository questionWithOptionAnswerRepository;
        private readonly IFormRepository formRepository;

        public GetQuestionsForFormCommandHandler(IQuestionWithOptionAnswerRepository questionWithOptionAnswerRepository, IFormRepository formRepository)
        {
            this.questionWithOptionAnswerRepository = questionWithOptionAnswerRepository;
            this.formRepository = formRepository;
        }

        public async Task<IEnumerable<QuestionWithOptionAnswer>> Handle(GetQuestionsForFormCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await formRepository.Exists(x => x.Id == request.FormId);
            if (formExists)
            {
                 return await questionWithOptionAnswerRepository.GetQuestionsWithRelatedEntities(request.FormId);
            }
            throw new ItemNotFoundException("The form was not found...");
        }
    }
}
