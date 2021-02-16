using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<Teacher> GetTeacher(Guid id);
        Task Delete(Guid id);
        Task<IEnumerable<Teacher>> GetAllWithRelatedEntities();
        Task<IEnumerable<Teacher>> GetByDepartment(Department department);
        Task<Teacher> GetByUserId(Guid userId);
    }
}
