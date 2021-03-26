using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Form
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
        public int MinNumberOfAttendances { get; set; }
    }
}