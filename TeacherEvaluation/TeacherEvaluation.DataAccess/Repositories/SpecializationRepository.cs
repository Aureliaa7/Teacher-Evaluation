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
    public class SpecializationRepository : Repository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Specialization>> GetAllWithRelatedEntitiesAsync()
        {
            return await Context.Set<Specialization>()
                .Include(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Specialization> GetSpecializationAsync(Guid id)
        {
            return await Context.Set<Specialization>()
                .Where(x => x.Id == id)
                .Include(x => x.StudyDomain)
                .FirstAsync();
        }

        public async Task<Specialization> GetByNameAsync(string name)
        {
            return await Context.Set<Specialization>()
                .Where(x => x.Name.Equals(name))
                .Include(x => x.StudyDomain)
                .FirstOrDefaultAsync();
        }
    }
}
