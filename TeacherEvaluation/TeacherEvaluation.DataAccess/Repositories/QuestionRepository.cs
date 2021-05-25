using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Question>> GetQuestionsWithRelatedEntitiesAsync(Guid formId)
        {
            return await Context.Set<Question>()
                .Where(x => x.Form.Id == formId)
                .Include(x => x.Form)
                .ToListAsync();
        }
    }
}
