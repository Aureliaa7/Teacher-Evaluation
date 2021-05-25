using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        { 
        }

        public async Task<Student> GetStudentAsync(Guid id)
        {
            return await Context.Set<Student>()
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .FirstAsync();
        }

        public async Task DeleteAsync(Guid id)
        { 
            var studentToBeDeleted = await GetStudentAsync(id);
            var userToBeDeleted = studentToBeDeleted.User;
            var enrollments = Context.Set<Enrollment>().Where(x => x.Student.Id == id);
            var grades = enrollments.Select(x => x.Grade);
            Context.Set<Enrollment>().RemoveRange(enrollments);
            Context.Set<Grade>().RemoveRange(grades);
            Context.Set<Student>().Remove(studentToBeDeleted);
            Context.Set<ApplicationUser>().Remove(userToBeDeleted);
        }

        public async Task<IEnumerable<Student>> GetAllWithRelatedEntitiesAsync()
        {
            return await Context.Set<Student>()
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> GetByUserIdAsync(Guid id)
        {
            return await Context.Set<Student>()
                .Where(x => x.User.Id == id)
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .FirstAsync();
        }

        public async Task<IEnumerable<Student>> GetByCriteriaWithRelatedEntitiesAsync(StudyProgramme studyProgramme, 
            Guid studyDomainId, Guid specializationId, int studyYear)
        {
            return await Context.Set<Student>()
                .Where(x => x.Specialization.Id == specializationId &&
                       x.Specialization.StudyDomain.StudyProgramme == studyProgramme &&
                       x.Specialization.StudyDomain.Id == studyDomainId &&
                       x.StudyYear == studyYear)
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .ToListAsync();
        }
    }
}
