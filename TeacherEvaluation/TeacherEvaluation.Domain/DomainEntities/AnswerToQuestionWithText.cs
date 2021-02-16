using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class AnswerToQuestionWithText
    {
        public Guid Id { get; set; }
        public Enrollment Enrollment { get; set; }
        public string Answer { get; set; }
        public Question Question { get; set; }
    }
}
