using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Subject>> GetAllWithRelatedEntitiesAsync()
        {
            return await Context.Set<Subject>()
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByCriteriaAsync(Guid specializationId, int studyYear)
        {
            return await Context.Set<Subject>()
                .Where(x => x.StudyYear == studyYear && x.Specialization.Id == specializationId)
                .Include(x => x.Specialization)
                    .ThenInclude(x => x.StudyDomain)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Subject> GetWithRelatedEntitiesAsync(Guid id)
        {
            return await Context.Set<Subject>()
               .Where(x => x.Id == id)
               .Include(x => x.Specialization)
                   .ThenInclude(x => x.StudyDomain)
               .FirstAsync();
        }

        //This has to be done because when deleting a subject, the SubjectId from TaughtSubjects table will be set to null
        // and when deleting a taught subject, the TaughtSubjectId from Enrollments table is also set to null. 
        // So delete everything that's related to this subject.
        public async Task DeleteAsync(Guid id)
        {
            var taughtSubjects = Context.Set<TaughtSubject>().Where(ts => ts.Subject.Id == id);
            var enrollments = new List<Enrollment>();
            foreach(var ts in taughtSubjects)
            {
                var _enrollments = Context.Set<Enrollment>().Where(e => e.TaughtSubject.Id == ts.Id);
                enrollments.AddRange(_enrollments);
            }
            var subjectToBeDeleted = await Context.Set<Subject>().FirstOrDefaultAsync(s => s.Id == id);
            Context.Set<Subject>().Remove(subjectToBeDeleted);
            Context.Set<TaughtSubject>().RemoveRange(taughtSubjects);
            Context.Set<Enrollment>().RemoveRange(enrollments);
        }
    }
}
