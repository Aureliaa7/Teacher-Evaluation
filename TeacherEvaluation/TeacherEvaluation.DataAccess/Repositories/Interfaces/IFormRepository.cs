using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<Form> GetByDate(DateTime currentDate);
        Task<IEnumerable<Form>> GetAllByType(FormType type);
    }
}
