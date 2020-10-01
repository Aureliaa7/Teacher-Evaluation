using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Evaluations.Forms
{
    internal static class AnswerOptionConvertor
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
