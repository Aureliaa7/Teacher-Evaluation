using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IAnswerToQuestionWithTextRepository : IRepository<AnswerToQuestionWithText>
    {
        Task<IEnumerable<AnswerToQuestionWithText>> GetByEnrollmentAndFormId(Guid enrollmentId, Guid formId);
        Task<IEnumerable<AnswerToQuestionWithText>> GetByQuestionId(Guid id);
    }
}
