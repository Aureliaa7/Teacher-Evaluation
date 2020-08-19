using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface ITaughtSubjectRepository : IRepository<TaughtSubject>
    {
        Task<TaughtSubject> GetTaughtSubject(Guid id);
        Task<IEnumerable<TaughtSubject>> GetAllWithRelatedEntities();
    }
}
