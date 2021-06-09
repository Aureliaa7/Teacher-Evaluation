using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfCredits { get; set; }
        public int StudyYear { get; set; }
        public Specialization Specialization { get; set; }
        public Semester Semester { get; set; }
    }
}
