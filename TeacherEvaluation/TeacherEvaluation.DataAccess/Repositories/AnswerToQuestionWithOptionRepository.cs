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
    public class AnswerToQuestionWithOptionRepository : Repository<AnswerToQuestionWithOption>, IAnswerToQuestionWithOptionRepository
    {
        public AnswerToQuestionWithOptionRepository(ApplicationDbContext context) : base(context)
        {}

        public async Task<IEnumerable<AnswerToQuestionWithOption>> GetByEnrollmentAndFormIdAsync(Guid enrollmentId, Guid formId)
        {
            return await Context.Set<AnswerToQuestionWithOption>()
                .Where(x => x.Enrollment.Id == enrollmentId && x.Question.Form.Id == formId)
                .Include(x => x.Question)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnswerToQuestionWithOption>> GetByQuestionIdAndTeacherIdAsync(Guid questionId, Guid teacherId)
        {
            return await Context.Set<AnswerToQuestionWithOption>()
                .Where(x => x.Question.Id == questionId && 
                x.Enrollment.TaughtSubject.Teacher.Id == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnswerToQuestionWithOption>> GetByQuestionIdAsync(Guid id)
        {
            return await Context.Set<AnswerToQuestionWithOption>()
                .Where(x => x.Question.Id == id)
                .Include(x => x.Enrollment)
                    .ThenInclude(x => x.TaughtSubject)
                        .ThenInclude(x => x.Teacher)
                .ToListAsync();
        }
    }
}
