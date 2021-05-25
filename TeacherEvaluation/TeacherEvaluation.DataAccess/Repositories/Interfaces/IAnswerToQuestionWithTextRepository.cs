using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IAnswerToQuestionWithTextRepository : IRepository<AnswerToQuestionWithText>
    {
        Task<IEnumerable<AnswerToQuestionWithText>> GetByEnrollmentAndFormIdAsync(Guid enrollmentId, Guid formId);
        Task<IEnumerable<AnswerToQuestionWithText>> GetByQuestionIdAsync(Guid id);
    }
}
