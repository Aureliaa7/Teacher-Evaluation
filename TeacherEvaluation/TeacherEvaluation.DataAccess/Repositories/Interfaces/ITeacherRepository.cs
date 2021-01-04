using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<Teacher> GetTeacher(Guid id);
        Task Delete(Guid id);
        Task<IEnumerable<Teacher>> GetAllWithRelatedEntities();
        Task<IEnumerable<Teacher>> GetByDepartment(Department department);
    }
}
