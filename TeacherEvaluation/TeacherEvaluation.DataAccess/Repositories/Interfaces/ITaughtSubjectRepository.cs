using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ITaughtSubjectRepository : IRepository<TaughtSubject>
    {
        Task<TaughtSubject> GetTaughtSubjectAsync(Guid id);
        Task<IEnumerable<TaughtSubject>> GetAllWithRelatedEntitiesAsync();
        Task<TaughtSubject> GetTaughtSubjectAsync(Guid teacherId, Guid subjectId, TaughtSubjectType type);
        Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByCriteriaAsync(Expression<Func<TaughtSubject, bool>> predicate);
    }
}
