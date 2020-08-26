using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public TaughtSubjectType Type { get; set; }
        public DateTime DateTime { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
