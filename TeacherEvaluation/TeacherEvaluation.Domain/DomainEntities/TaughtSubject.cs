using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class TaughtSubject
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public TaughtSubjectType Type { get; set; }
        public int MaxNumberOfAttendances { get; set; }
    }
}
