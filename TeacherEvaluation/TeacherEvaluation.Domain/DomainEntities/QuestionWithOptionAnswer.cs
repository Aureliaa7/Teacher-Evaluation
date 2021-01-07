using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class QuestionWithOptionAnswer
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public Form Form { get; set; }
    }
}
