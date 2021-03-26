using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<TaughtSubject> TaughtSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudyDomain> StudyDomains { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerToQuestionWithOption> AnswerToQuestionWithOptions { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<AnswerToQuestionWithText> AnswerToQuestionWithTexts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
