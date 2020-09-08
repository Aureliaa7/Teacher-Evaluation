using System;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Student
    {
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public string PIN { get; set; }
        public int StudyYear { get; set; }
        public Specialization Specialization { get; set; }
        public string Group { get; set; }
    }
}
