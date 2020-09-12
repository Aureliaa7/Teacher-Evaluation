using System;
using System.Collections.Generic;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class QuestionWithOptionAnswer
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public Form Form { get; set; }
        public ICollection<AnswerToQuestionWithOption> Answers { get; set; }
        
        public QuestionWithOptionAnswer()
        {
            Answers = new List<AnswerToQuestionWithOption>();
        }
    }
}
