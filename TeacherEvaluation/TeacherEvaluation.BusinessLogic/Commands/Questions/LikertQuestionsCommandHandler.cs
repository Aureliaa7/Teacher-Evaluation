using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Questions
{
    public class LikertQuestionsCommandHandler : IRequestHandler<LikertQuestionsCommand, IEnumerable<Question>>
    {
        private readonly IUnitOfWork unitOfWork;

        public LikertQuestionsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Question>> Handle(LikertQuestionsCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(f => f.Id == request.FormId);
            if (formExists)
            {
                var likertQuestions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntitiesAsync(request.FormId))
                                      .Where(q => !q.HasFreeFormAnswer);
                return likertQuestions;
            }
            throw new ItemNotFoundException("The form was not found...");
        }
    }
}
