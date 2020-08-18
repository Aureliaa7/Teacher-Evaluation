using System;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetStudent(Guid id);
        Task Delete(Guid id);
    }
}
