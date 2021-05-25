using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<Teacher> GetTeacherAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Teacher>> GetAllWithRelatedEntitiesAsync();
        Task<IEnumerable<Teacher>> GetByDepartmentAsync(Department department);
        Task<Teacher> GetByUserIdAsync(Guid userId);
    }
}
