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
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Subject>> GetAllWithRelatedEntities()
        {
            return await Context.Set<Subject>()
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByCriteria(Guid specializationId, int studyYear)
        {
            return await Context.Set<Subject>()
                .Where(x => x.StudyYear == studyYear && x.Specialization.Id == specializationId)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Subject> GetWithRelatedEntities(Guid id)
        {
            return await Context.Set<Subject>()
               .Where(x => x.Id == id)
               .Include(x => x.Specialization)
                   .ThenInclude(x => x.StudyDomain)
               .FirstAsync();
        }
    }
}
