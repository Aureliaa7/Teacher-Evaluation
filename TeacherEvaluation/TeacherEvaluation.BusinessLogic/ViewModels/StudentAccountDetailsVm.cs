namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class StudentAccountDetailsVm : UserAccountDetailsVm
    {
        public int StudyYear { get; init; }
        public string Group { get; init; }
        public string Specialization { get; init; }
        public string StudyDomain { get; init; }
        public string StudyProgramme { get; init; }
    }
}