using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Enrollment> GetEnrollmentAsync(Guid id)
        {
            return await Context.Set<Enrollment>()
               .Where(x => x.Id == id && x.TaughtSubject != null)
               .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Student)
                    .ThenInclude(x => x.Specialization)
                        .ThenInclude(x => x.StudyDomain)
                .Include(x => x.Grade)
                .FirstAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllWithRelatedEntitiesAsync()
        {
            return await Context.Set<Enrollment>()
                .Where(x => x.TaughtSubject != null)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Student)
                    .ThenInclude(x => x.Specialization)
                        .ThenInclude(x => x.StudyDomain)
                .Include(x => x.Grade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetForStudentAsync(Guid studentId)
        {
            return await Context.Set<Enrollment>()
                .Where(x => x.Student.Id == studentId && x.TaughtSubject != null)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Student)
                    .ThenInclude(x => x.Specialization)
                        .ThenInclude(x => x.StudyDomain)
                .Include(x => x.Grade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsForTaughtSubjectAsync(Guid id)
        {
            return await Context.Set<Enrollment>()
                .Where(x => x.TaughtSubject.Id == id)
                 .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Student)
                    .ThenInclude(x => x.Specialization)
                        .ThenInclude(x => x.StudyDomain)
                .Include(x => x.Grade)
                .ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentBySubjectStateTypeAndStudentAsync(
            Guid subjectId, 
            EnrollmentState state, 
            TaughtSubjectType type, 
            Guid studentId)
        {
            return await Context.Set<Enrollment>()
                .Where(x => x.TaughtSubject != null &&
                    x.TaughtSubject.Subject.Id == subjectId && 
                    x.State == state && x.Student.Id == studentId && 
                    x.TaughtSubject.Type == type)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Student)
                    .ThenInclude(x => x.Specialization)
                        .ThenInclude(x => x.StudyDomain)
                .Include(x => x.Grade)
                .FirstOrDefaultAsync();
        }

        public async Task<Enrollment> GetEnrollmentBySubjectSemesterTypeAndStudentAsync(
           Guid subjectId,
           Semester semester,
           TaughtSubjectType type,
           Guid studentId)
        {
            return await Context.Set<Enrollment>()
                .Where(x => x.TaughtSubject != null &&
                    x.TaughtSubject.Subject.Id == subjectId &&
                    x.Semester == semester && x.Student.Id == studentId &&
                    x.TaughtSubject.Type == type)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Student)
                    .ThenInclude(x => x.Specialization)
                        .ThenInclude(x => x.StudyDomain)
                .Include(x => x.Grade)
                .FirstOrDefaultAsync();
        }
    }
}
