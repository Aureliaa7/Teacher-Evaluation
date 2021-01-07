using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Grade
    {
        public Guid Id { get; set; }
        public int? Value { get; set; }
        public DateTime? Date { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
