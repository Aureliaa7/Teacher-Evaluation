using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class SaveEvaluationFormResponsesCommand : IRequest
    {
        public QuestionsVm Questions { get; set; }
        public List<AnswerOption> Responses { get; set; }
        public Guid FormId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid UserIdForStudent { get; set; }
        public TaughtSubjectType SubjectType { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
        public List<string> FreeFormAnswers { get; set; }
    }
}
