using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesWithRelatedEntities(Guid enrollmentId)
        {
            return await Context.Set<Attendance>().Where(x => x.Enrollment.Id == enrollmentId)
                                                  .Include(x => x.Enrollment)
                                                    .ThenInclude(x => x.TaughtSubject)
                                                        .ThenInclude(x => x.Subject)
                                                   .Include(x => x.Enrollment)
                                                    .ThenInclude(x => x.TaughtSubject)
                                                        .ThenInclude(x => x.Teacher).ToListAsync();
        }
    }
}
