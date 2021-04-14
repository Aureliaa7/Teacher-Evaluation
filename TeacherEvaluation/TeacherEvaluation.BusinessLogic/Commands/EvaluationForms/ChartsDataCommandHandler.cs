﻿using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities.Enums;

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
            bool formExists = await unitOfWork.FormRepository.Exists(f => f.Id == request.FormId);
            bool teacherExists = await unitOfWork.TeacherRepository.Exists(t => t.Id == request.TeacherId);
            if (formExists && teacherExists)
            {
                IDictionary<string, IDictionary<string, int>> questionsWithResponses = new Dictionary<string, IDictionary<string, int>>();
                var questions = (await unitOfWork.QuestionRepository.GetQuestionsWithRelatedEntities(request.FormId))
                                .Where(q => !q.HasFreeFormAnswer)
                                .ToList();

                foreach(var question in questions)
                {
                    var responses = (await unitOfWork.AnswerToQuestionWithOptionRepository.GetByQuestionId(question.Id))
                                     .Where(r => r.Enrollment.TaughtSubject.Teacher.Id == request.TeacherId)
                                     .Select(x => x.Answer)
                                     .ToList();

                    int noStronglyDisagreeAnswers = (responses.Where(r => r.Equals(AnswerOption.StronglyDisagree))).Count();
                    int noDisagreeAnswers = (responses.Where(r => r.Equals(AnswerOption.Disagree))).Count();
                    int noNeutralAnswers = (responses.Where(r => r.Equals(AnswerOption.Neutral))).Count();
                    int noAgreeAnswers = (responses.Where(r => r.Equals(AnswerOption.Agree))).Count();
                    int noStronglyAgreeAnswers = (responses.Where(r => r.Equals(AnswerOption.StronglyAgree))).Count();

                    IDictionary<string, int> answersOptionAndNumberOfAnswers = new Dictionary<string, int>();

                    answersOptionAndNumberOfAnswers.Add("Strongly Disagree", noStronglyDisagreeAnswers);
                    answersOptionAndNumberOfAnswers.Add("Disagree", noDisagreeAnswers);
                    answersOptionAndNumberOfAnswers.Add("Neutral", noNeutralAnswers);
                    answersOptionAndNumberOfAnswers.Add("Agree", noAgreeAnswers);
                    answersOptionAndNumberOfAnswers.Add("Strongly Agree", noStronglyAgreeAnswers);
                    questionsWithResponses.Add(question.Text, answersOptionAndNumberOfAnswers);
                }

                return questionsWithResponses;
            }
            throw new ItemNotFoundException("Not found");
        }
    }
}