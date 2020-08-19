using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class TaughtSubjectRepository : Repository<TaughtSubject>, ITaughtSubjectRepository
    {
        public TaughtSubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TaughtSubject> GetTaughtSubject(Guid id)
        {
            return await Context.Set<TaughtSubject>()
                .Where(x => x.Id == id)
                .Include(entity => entity.Teacher)
                    .ThenInclude(teacher => teacher.User)
                .Include(entity => entity.Subject)
                .FirstAsync();
        }
    }
}
