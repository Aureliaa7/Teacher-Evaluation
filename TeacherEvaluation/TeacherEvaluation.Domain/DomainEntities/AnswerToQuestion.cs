using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class AnswerToQuestion
    {
        public Guid Id { get; set; }
        public Enrollment Enrollment { get; set; }
        public Question Question { get; set; }
        public bool IsFreeForm { get; set; }
    }
}
