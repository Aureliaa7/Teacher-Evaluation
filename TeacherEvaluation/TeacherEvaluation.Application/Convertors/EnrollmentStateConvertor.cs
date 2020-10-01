using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Convertors
{
    internal static class EnrollmentStateConvertor
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
