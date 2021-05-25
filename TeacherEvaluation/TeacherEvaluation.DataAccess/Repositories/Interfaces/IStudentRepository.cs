using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetStudentAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Student>> GetAllWithRelatedEntitiesAsync();
        Task<Student> GetByUserIdAsync(Guid id);
        Task<IEnumerable<Student>> GetByCriteriaWithRelatedEntitiesAsync(StudyProgramme studyProgramme, 
            Guid studyDomainId, Guid specializationId, int studyYear);
    }
}
