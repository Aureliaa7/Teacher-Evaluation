using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<Enrollment> GetEnrollmentAsync(Guid id);
        Task<IEnumerable<Enrollment>> GetAllWithRelatedEntitiesAsync();
        Task<IEnumerable<Enrollment>> GetForStudentAsync(Guid studentId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsForTaughtSubjectAsync(Guid id);
        Task<Enrollment> GetEnrollmentBySubjectStateTypeAndStudentAsync(
            Guid subjectId, 
            EnrollmentState state, 
            TaughtSubjectType subjectType, 
            Guid studentId);
        Task<Enrollment> GetEnrollmentBySubjectSemesterTypeAndStudentAsync(
            Guid subjectId, 
            Semester semester,
            TaughtSubjectType subjectType, 
            Guid studentId);
    }
}
