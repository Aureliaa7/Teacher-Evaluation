using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetQuestionsForFormCommandHandler : IRequestHandler<GetQuestionsForFormCommand, QuestionsVm>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetQuestionsForFormCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QuestionsVm> Handle(GetQuestionsForFormCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(x => x.Id == request.FormId);
            if (formExists)
            {
                var questions = await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId);
                var questionsVm = new QuestionsVm
                {
                    FreeFormQuestions = questions.Where(q => q.HasFreeFormAnswer),
                    LikertQuestions = questions.Where(q => !q.HasFreeFormAnswer)
                };
                return questionsVm;
            }
            throw new ItemNotFoundException("The form was not found...");
        }
    }
}
