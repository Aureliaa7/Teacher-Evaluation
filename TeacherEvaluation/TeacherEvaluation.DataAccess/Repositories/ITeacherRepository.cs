using System;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<Teacher> GetTeacher(Guid id);
        Task Delete(Guid id);
    }
}
