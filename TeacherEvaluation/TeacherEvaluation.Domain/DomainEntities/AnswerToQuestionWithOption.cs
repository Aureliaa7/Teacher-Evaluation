using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class AnswerToQuestionWithOption
    {
        public Guid Id { get; set; }
        public Enrollment Enrollment { get; set; }
        public AnswerOption Answer { get; set; }
        public QuestionWithOptionAnswer Question { get; set; }
    }
}