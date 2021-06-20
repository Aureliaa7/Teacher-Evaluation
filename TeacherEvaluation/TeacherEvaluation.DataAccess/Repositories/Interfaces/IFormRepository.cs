using System;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<Form> GetByDateAsync(DateTime currentDate);
    }
}
