using System;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Domain.DomainEntities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public string PIN { get; set; }
        public string Degree { get; set; }
        public Department Department { get; set; }
    }
}
