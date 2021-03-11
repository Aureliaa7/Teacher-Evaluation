using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<Subject> GetWithRelatedEntities(Guid id);
        Task<IEnumerable<Subject>> GetAllWithRelatedEntities();
    }
}
