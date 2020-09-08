using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class SpecializationRepository : Repository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Specialization>> GetAllWithRelatedEntities()
        {
            return await Context.Set<Specialization>()
                .Include(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Specialization> GetSpecialization(Guid id)
        {
            return await Context.Set<Specialization>()
                .Where(x => x.Id == id)
                .Include(x => x.StudyDomain)
                .FirstAsync();
        }
    }
}
