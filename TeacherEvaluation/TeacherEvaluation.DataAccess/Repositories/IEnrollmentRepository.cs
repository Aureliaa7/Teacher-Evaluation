using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<Enrollment> GetEnrollment(Guid id);
        Task<IEnumerable<Enrollment>> GetAllWithRelatedEntities();
        Task<IEnumerable<Enrollment>> GetForStudent(Guid studentId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsForTaughtSubject(Guid id);
    }
}
