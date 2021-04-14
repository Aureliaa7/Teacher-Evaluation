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
    public class AnswerToQuestionWithTextRepository : Repository<AnswerToQuestionWithText>, IAnswerToQuestionWithTextRepository
    {
        public AnswerToQuestionWithTextRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<AnswerToQuestionWithText>> GetByEnrollmentAndFormId(Guid enrollmentId, Guid formId)
        {
            return await Context.Set<AnswerToQuestionWithText>()
                .Where(x => x.Enrollment.Id == enrollmentId && x.Question.Form.Id == formId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnswerToQuestionWithText>> GetByQuestionId(Guid id)
        {
            return await Context.Set<AnswerToQuestionWithText>()
                .Where(x => x.Question.Id == id)
                .Include(x => x.Enrollment)
                    .ThenInclude(x => x.TaughtSubject)
                        .ThenInclude(x => x.Teacher)
                .ToListAsync();
        }
    }
}
