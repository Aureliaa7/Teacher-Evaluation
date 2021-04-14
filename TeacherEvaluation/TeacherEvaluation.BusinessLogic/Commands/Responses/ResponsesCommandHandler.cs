using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommandHandler : IRequestHandler<ResponsesCommand, IEnumerable<IDictionary<string, string>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResponsesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        //TODO maybe I can reuse some code from ChartsDataCommandHandler
        public async Task<IEnumerable<IDictionary<string, string>>> Handle(ResponsesCommand request, CancellationToken cancellationToken)
        {
            // 1. take the questions based on form id
            var questions = await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId);
            var questionsIds = questions.Select(q => q.Id);

            // get all the responses based on the form id
            var responses = await unitOfWork.AnswerRepository.GetByFormIdAsync(request.FormId);
            var enrollmentIds = responses.Select(r => r.Enrollment.Id).Distinct();

            // based on questionsIds, get the answers
            //var answers = await unitOfWork.AnswerRepository.

            // 2. based on the questions previously retrieved and based on the teacher id, take the enrollment ids 
            // 3.


            throw new System.NotImplementedException();
        }
    }
}
