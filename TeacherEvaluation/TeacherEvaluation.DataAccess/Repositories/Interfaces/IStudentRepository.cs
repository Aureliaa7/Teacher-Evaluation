using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetStudent(Guid id);
        Task Delete(Guid id);
        Task<IEnumerable<Student>> GetAllWithRelatedEntities();
        Task<Student> GetByUserId(Guid id);
        Task<IEnumerable<Student>> GetByCriteriaWithRelatedEntities(StudyProgramme studyProgramme, 
            Guid studyDomainId, Guid specializationId, int studyYear);
    }
}
