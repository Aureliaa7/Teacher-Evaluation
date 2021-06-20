using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class StudyDomainRepository : Repository<StudyDomain>, IStudyDomainRepository
    {
        public StudyDomainRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StudyDomain>> GetByStudyProgrammeAsync(StudyProgramme studyProgramme)
        {
            return await Context.Set<StudyDomain>()
                .Where(x => x.StudyProgramme == studyProgramme)
                .ToListAsync();
        }
    }
}
