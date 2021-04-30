using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Convertors
{
    public static class AnswerOptionConvertor
    {
        public static string ToDisplayString(AnswerOption answer)
        {
            return answer switch
            {
                AnswerOption.StronglyDisagree => "Strongly Disagree",
                AnswerOption.Disagree => "Disagree",
                AnswerOption.Neutral => "Neutral", 
                AnswerOption.Agree => "Agree",
                AnswerOption.StronglyAgree => "Strongly Agree",
                _ => "Unknown Answer"
            };
        }
    }
}
