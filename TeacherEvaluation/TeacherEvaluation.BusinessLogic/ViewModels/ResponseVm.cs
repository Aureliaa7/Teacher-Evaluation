using System;

namespace TeacherEvaluation.BusinessLogic.ViewModels
{
    public class ResponseVm
    {
        public Guid EnrollmentId { get; set; }
        public int NoAttendances { get; set; }
        public int Grade { get; set; }
    }
}
