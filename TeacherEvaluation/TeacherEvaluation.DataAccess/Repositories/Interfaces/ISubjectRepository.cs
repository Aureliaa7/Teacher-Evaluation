using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<Subject> GetWithRelatedEntitiesAsync(Guid id);
        Task<IEnumerable<Subject>> GetAllWithRelatedEntitiesAsync();
        Task<IEnumerable<Subject>> GetSubjectsByCriteriaAsync(Guid specializationId, int studyYear);
        Task DeleteAsync(Guid id);
    }
}
