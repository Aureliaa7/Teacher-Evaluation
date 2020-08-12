using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddRoles(modelBuilder);
        }

        private void AddRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                 new ApplicationRole
                 {
                     Id = Guid.NewGuid(),
                     Name = "Dean",
                     NormalizedName = "DEAN"
                 },
                 new ApplicationRole
                 {
                     Id = Guid.NewGuid(),
                     Name = "Student",
                     NormalizedName = "STUDENT"
                 },
                   new ApplicationRole
                   {
                       Id = Guid.NewGuid(),
                       Name = "Teacher",
                       NormalizedName = "TEACHER"
                   }
                );
        }
    }
}
