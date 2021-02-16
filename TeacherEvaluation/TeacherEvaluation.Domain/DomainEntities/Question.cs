using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Form Form { get; set; }
    }
}
