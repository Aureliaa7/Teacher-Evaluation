using System;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<Form> GetByDate(DateTime currentDate);
    }
}
