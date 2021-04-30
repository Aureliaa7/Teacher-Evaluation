using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Convertors
{
    public static class EnrollmentStateConvertor
    {
        public static string ToDisplayString(EnrollmentState state)
        {
            return state switch {
                EnrollmentState.Done => "Done",
                EnrollmentState.InProgress => "In Progress",
                _ => "Unknown State"
            };
        }
    }
}
