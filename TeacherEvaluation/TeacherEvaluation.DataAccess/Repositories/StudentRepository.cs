using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        { 
        }

        public async Task<Student> GetStudent(Guid id)
        {
            return await Context.Set<Student>()
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .FirstAsync();
        }

        public async Task Delete(Guid id)
        { 
            var studentToBeDeleted = await GetStudent(id);
            var userToBeDeleted = studentToBeDeleted.User;
            var enrollments = Context.Set<Enrollment>().Where(x => x.Student.Id == id);
            var grades = enrollments.Select(x => x.Grade);
            Context.Set<Enrollment>().RemoveRange(enrollments);
            Context.Set<Grade>().RemoveRange(grades);
            Context.Set<Student>().Remove(studentToBeDeleted);
            Context.Set<ApplicationUser>().Remove(userToBeDeleted);

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllWithRelatedEntities()
        {
            return await Context.Set<Student>()
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> GetByUserId(Guid id)
        {
            return await Context.Set<Student>()
                .Where(x => x.User.Id == id)
                .Include(x => x.User)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .FirstAsync();
        }

        public async Task<IEnumerable<Student>> GetByCriteriaWithRelatedEntities(StudyProgramme studyProgramme, 
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
