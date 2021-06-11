using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class TakenSubjectVm
    {
        public string SubjectTitle { get; set; }
        public int NumberOfCredits { get; set; }
        public string TeacherName { get; set; }
        public int NumberOfAttendances { get; set; }
        public int StudyYear { get; set; }
        public Semester Semester { get; set; }
    }
}
