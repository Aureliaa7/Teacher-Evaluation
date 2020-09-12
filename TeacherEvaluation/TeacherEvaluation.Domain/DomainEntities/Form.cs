using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Form
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FormType Type { get; set; }
        public EnrollmentState ForEnrollmentState { get; set; }
    }
}