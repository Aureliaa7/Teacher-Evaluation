using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class QuestionWithOptionAnswerRepository : Repository<QuestionWithOptionAnswer>, IQuestionWithOptionAnswerRepository
    {
        public QuestionWithOptionAnswerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<QuestionWithOptionAnswer>> GetQuestionsWithRelatedEntities(Guid formId)
        {
            return await Context.Set<QuestionWithOptionAnswer>()
                .Where(x => x.Form.Id == formId)
                .Include(x => x.Answers)
                    .ThenInclude(x => x.Enrollment)
                .ToListAsync();
        }
    }
}
