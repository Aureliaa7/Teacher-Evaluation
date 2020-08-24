using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Enrollment> GetEnrollment(Guid id)
        {
            return await Context.Set<Enrollment>()
               .Where(x => x.Id == id)
               .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Grade)
                .FirstAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllWithRelatedEntities()
        {
            return await Context.Set<Enrollment>()
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Grade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetForStudent(Guid studentId)
        {
            return await Context.Set<Enrollment>()
                .Where(x => x.Student.Id == studentId)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .Include(x => x.TaughtSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Grade)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsForTaughtSubject(Guid id)
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
                .Include(x => x.Grade)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
