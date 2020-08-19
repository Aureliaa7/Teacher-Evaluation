using System;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface ITaughtSubjectRepository : IRepository<TaughtSubject>
    {
        Task<TaughtSubject> GetTaughtSubject(Guid id);
    }
}
