using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class ChartsDataCommandHandler : IRequestHandler<ChartsDataCommand, IDictionary<string, IDictionary<string, int>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ChartsDataCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<string, IDictionary<string, int>>> Handle(ChartsDataCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.ExistsAsync(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.ExistsAsync(t => t.Id == request.TeacherId);
            if (formExists && teacherExists)
            {
                var questions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId))
                         .Where(q => !q.HasFreeFormAnswer)
                         .ToList();

                if (request.TaughtSubjectId.Equals("All"))
                {
                    return await GetChartsDataOverall(request.TeacherId, questions);
                }
                // "Please select" is selected
                else if (request.TaughtSubjectId.Equals("default"))
                {
                    return new Dictionary<string, IDictionary<string, int>>();
                }
                else if (await unitOfWork.TaughtSubjectRepository.ExistsAsync(ts => ts.Id == new Guid(request.TaughtSubjectId)))
                {
                    return await GetChartsDataForTaughtSubject(request.TeacherId, new Guid(request.TaughtSubjectId), questions);
                }
            }
            throw new ItemNotFoundException("Not found");
        }

        private async Task<IDictionary<string, IDictionary<string, int>>> GetChartsDataOverall(Guid teacherId, List<Question> questions)
        {
            IDictionary<string, IDictionary<string, int>> questionsWithResponses = new Dictionary<string, IDictionary<string, int>>();

            foreach (var question in questions)
            {
                var responses = (await unitOfWork.AnswerToQuestionWithOptionRepository.GetByQuestionIdAsync(question.Id))
                                 .Where(r => r.Enrollment.TaughtSubject.Teacher.Id == teacherId)
                                 .Select(x => x.Score)
                                 .ToList();

                questionsWithResponses.Add(question.Text, GetAnswersOptionAndNumberOfAnswers(responses));
            }

            return questionsWithResponses;
        }

        private async Task<IDictionary<string, IDictionary<string, int>>> GetChartsDataForTaughtSubject(Guid teacherId, Guid taughtSubjectId, List<Question> questions)
        {
            IDictionary<string, IDictionary<string, int>> questionsWithResponses = new Dictionary<string, IDictionary<string, int>>();

            foreach (var question in questions)
            {
                var scores = (await unitOfWork.AnswerToQuestionWithOptionRepository.GetByQuestionIdAsync(question.Id))
                                 .Where(r => r.Enrollment.TaughtSubject.Teacher.Id == teacherId &&
                                        r.Enrollment.TaughtSubject.Id == taughtSubjectId)
                                 .Select(x => x.Score)
                                 .ToList();

                questionsWithResponses.Add(question.Text, GetAnswersOptionAndNumberOfAnswers(scores));
            }

            return questionsWithResponses;
        }

        private IDictionary<string, int> GetAnswersOptionAndNumberOfAnswers(List<int> scores)
        {
            IDictionary<string, int> answersOptionAndNumberOfAnswers = new Dictionary<string, int>();

            for (int i = 1; i < 11; i++)
            {
                answersOptionAndNumberOfAnswers.Add(i.ToString(), (scores.Where(r => r == i)).Count());
            }

            return answersOptionAndNumberOfAnswers;
        }
    }
}