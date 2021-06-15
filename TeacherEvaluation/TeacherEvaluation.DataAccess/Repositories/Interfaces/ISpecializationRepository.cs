using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        Task<IEnumerable<Specialization>> GetAllWithRelatedEntitiesAsync();
        Task<Specialization> GetSpecializationAsync(Guid id);
        Task<Specialization> GetByNameAsync(string name);
    }
}
