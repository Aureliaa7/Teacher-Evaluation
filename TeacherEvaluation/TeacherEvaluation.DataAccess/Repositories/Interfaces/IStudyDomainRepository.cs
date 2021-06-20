using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IStudyDomainRepository : IRepository<StudyDomain>
    {
        Task<IEnumerable<StudyDomain>> GetByStudyProgrammeAsync(StudyProgramme studyProgramme);
    }
}
