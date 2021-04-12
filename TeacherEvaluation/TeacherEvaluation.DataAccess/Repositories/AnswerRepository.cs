using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class AnswerRepository : Repository<AnswerToQuestion>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AnswerToQuestion>> GetByFormIdAsync(Guid id)
        {
            return await Context.Set<AnswerToQuestion>()
                .Where(a => a.Question.Id == id)
                .Include(a => a.Question)
                .Include(a => a.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(a => a.Enrollment)
                    .ThenInclude(e => e.TaughtSubject)
                        .ThenInclude(ts => ts.Teacher)
                .ToListAsync();
        }
    }
}
