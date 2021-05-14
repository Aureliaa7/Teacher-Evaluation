using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class AnswerToQuestionWithOption : AnswerToQuestion
    {
        public int Score { get; set; }
    }
}