using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface IQuestionWithOptionAnswerRepository : IRepository<QuestionWithOptionAnswer>
    {
        Task<IEnumerable<QuestionWithOptionAnswer>> GetQuestionsWithRelatedEntities(Guid formId);
    }
}
