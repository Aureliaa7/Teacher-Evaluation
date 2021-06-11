using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities.Enums;
using System.Linq.Expressions;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class TaughtSubjectRepository : Repository<TaughtSubject>, ITaughtSubjectRepository
    {
        public TaughtSubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TaughtSubject> GetTaughtSubjectAsync(Guid id)
        {
            return await Context.Set<TaughtSubject>()
                .Where(x => x.Id == id)
                .Include(entity => entity.Teacher)
                    .ThenInclude(teacher => teacher.User)
                .Include(entity => entity.Subject)
                    .ThenInclude(subject => subject.Specialization)
                        .ThenInclude(specialization => specialization.StudyDomain)
                .FirstAsync();
        }

        public async Task<IEnumerable<TaughtSubject>> GetAllWithRelatedEntitiesAsync()
        {
            return await Context.Set<TaughtSubject>()
                .Include(entity => entity.Teacher)
                    .ThenInclude(user => user.User)
                .Include(entity => entity.Subject)
                    .ThenInclude(subject => subject.Specialization)
                        .ThenInclude(specialization => specialization.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaughtSubject> GetTaughtSubjectAsync(Guid teacherId, Guid subjectId, TaughtSubjectType type)
        {
            return await Context.Set<TaughtSubject>()
              .Where(x => x.Teacher.Id == teacherId && x.Subject.Id == subjectId && x.Type == type)
              .Include(entity => entity.Teacher)
                  .ThenInclude(teacher => teacher.User)
              .Include(entity => entity.Subject)
                .ThenInclude(subject => subject.Specialization)
                        .ThenInclude(specialization => specialization.StudyDomain)
              .FirstAsync();
        }

        public async Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByCriteria(Expression<Func<TaughtSubject, bool>> predicate)
        {
            return await Context.Set<TaughtSubject>()
                .Where(predicate)
                .Include(entity => entity.Teacher)
                   .ThenInclude(teacher => teacher.User)
                .Include(entity => entity.Subject)
                    .ThenInclude(subject => subject.Specialization)
                        .ThenInclude(specialization => specialization.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
