using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class StudyDomain
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StudyProgramme StudyProgramme { get; set; }
    }
}
