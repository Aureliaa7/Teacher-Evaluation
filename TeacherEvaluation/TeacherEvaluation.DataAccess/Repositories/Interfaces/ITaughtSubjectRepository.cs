using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface ITaughtSubjectRepository : IRepository<TaughtSubject>
    {
        Task<TaughtSubject> GetTaughtSubject(Guid id);
        Task<IEnumerable<TaughtSubject>> GetAllWithRelatedEntities();
        Task<TaughtSubject> GetTaughtSubject(Guid teacherId, Guid subjectId, TaughtSubjectType type);
        Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByDepartmentAndType(Department department, TaughtSubjectType taughtSubjectType);
        Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsByTeacherIdAndType(Guid teacherId, TaughtSubjectType type);
        Task<IEnumerable<TaughtSubject>> GetTaughtSubjectsBySubjectIdAndType(Guid subjectId, TaughtSubjectType type);
    }
}
