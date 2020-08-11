using System;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string PIN { get; set; }
        public string Degree { get; set; }
        public Department Department { get; set; }
    }
}
