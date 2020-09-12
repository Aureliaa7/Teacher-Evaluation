using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class AnswerToQuestionWithOption
    {
        public Guid Id { get; set; }
        public Enrollment Enrollment { get; set; }
        public OptionAnswer Answer { get; set; }
        public Form Form { get; set; }
    }
}