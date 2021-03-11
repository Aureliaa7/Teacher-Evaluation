using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class TakenSubjectVm
    {
        public TaughtSubject TaughtSubject { get; set; }
        public int NumberOfAttendances { get; set; }
    }
}
