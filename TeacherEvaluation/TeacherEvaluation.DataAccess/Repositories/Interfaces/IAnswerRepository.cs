using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IAnswerRepository: IRepository<AnswerToQuestion>
    {
        Task<IEnumerable<AnswerToQuestion>> GetByFormIdAsync(Guid id);
    }
}
