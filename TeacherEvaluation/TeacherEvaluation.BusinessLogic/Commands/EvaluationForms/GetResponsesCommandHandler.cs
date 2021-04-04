using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetResponsesCommandHandler : IRequestHandler<GetResponsesCommand, IDictionary<string, IDictionary<string, int>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetResponsesCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, IDictionary<string, int>>> Handle(GetResponsesCommand request, CancellationToken cancellationToken)
        {
            IDictionary<string, IDictionary<string, int>> questionsWithResponses = new Dictionary<string, IDictionary<string, int>>();

            bool formExists = await unitOfWork.FormRepository.Exists(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.Id == request.TeacherId);
            if (formExists && teacherExists)
            {
                // take all the questions;
                // for the first 10 questions, take all the responses

                var questions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId)).ToList();
                for (int contor = 0; contor < Constants.NumberOfQuestionsWithAnswerOption; contor++)
                {
                    var responses = (await unitOfWork.AnswerToQuestionWithOptionRepository.GetByQuestionId(questions.ElementAt(contor).Id))
                                     .Select(x => x.Answer);
                    int noStronglyDisagreeAnswers = responses.Where(r => r.Equals(AnswerOption.StronglyDisagree)).Count();
                    int noDisagreeAnswers = responses.Where(r => r.Equals(AnswerOption.Disagree)).Count();
                    int noNeutralAnswers = responses.Where(r => r.Equals(AnswerOption.Neutral)).Count();
                    int noAgreeAnswers = responses.Where(r => r.Equals(AnswerOption.Agree)).Count();
                    int noStronglyAgreeAnswers = responses.Where(r => r.Equals(AnswerOption.StronglyAgree)).Count();

                    IDictionary<string, int> answersOptionAndNumberOfAnswers = new Dictionary<string, int>();
                    answersOptionAndNumberOfAnswers.Add("Strongly Disagree", noStronglyDisagreeAnswers);
                    answersOptionAndNumberOfAnswers.Add("Disagree", noDisagreeAnswers);
                    answersOptionAndNumberOfAnswers.Add("Neutral", noNeutralAnswers);
                    answersOptionAndNumberOfAnswers.Add("Agree", noAgreeAnswers);
                    answersOptionAndNumberOfAnswers.Add("Strongly Agree", noStronglyAgreeAnswers);

                    questionsWithResponses.Add(questions.ElementAt(contor).Text, answersOptionAndNumberOfAnswers);
                }

                return questionsWithResponses;
            }
            throw new ItemNotFoundException("Not found");
        }
    }
}
