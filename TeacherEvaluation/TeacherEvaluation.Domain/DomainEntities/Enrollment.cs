using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Student Student { get; set; }
        public TaughtSubject TaughtSubject { get; set; }
        public Grade Grade { get; set; }
        public EnrollmentState State { get; set; }
        public int NumberOfAttendances { get; set; }
        public Semester Semester { get; set; }
    }
}
