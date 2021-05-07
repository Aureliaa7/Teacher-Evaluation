using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IAnswerToQuestionWithOptionRepository : IRepository<AnswerToQuestionWithOption> 
    {
        Task<IEnumerable<AnswerToQuestionWithOption>> GetByEnrollmentAndFormIdAsync(Guid enrollmentId, Guid formId);
        Task<IEnumerable<AnswerToQuestionWithOption>> GetByQuestionIdAsync(Guid id);
        Task<IEnumerable<AnswerToQuestionWithOption>> GetByQuestionIdAndTeacherIdAsync(Guid questionId, Guid teacherId);
    }
}
