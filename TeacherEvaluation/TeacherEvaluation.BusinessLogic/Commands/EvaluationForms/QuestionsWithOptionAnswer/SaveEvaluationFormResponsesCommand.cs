using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class SaveEvaluationFormResponsesCommand : IRequest
    {
        public IEnumerable<Question> Questions { get; set; }
        public List<AnswerOption> Responses { get; set; }
        public Guid FormId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid UserIdForStudent { get; set; }
        public TaughtSubjectType SubjectType { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
    }
}
