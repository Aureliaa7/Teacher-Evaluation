using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StudyDomain StudyDomain { get; set; }
    }
}
