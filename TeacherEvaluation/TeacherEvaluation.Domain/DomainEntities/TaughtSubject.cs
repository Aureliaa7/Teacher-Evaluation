using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class TaughtSubject
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
