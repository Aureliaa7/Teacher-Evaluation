using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Teacher> GetTeacherAsync(Guid id)
        {
            return await Context.Set<Teacher>()
              .Where(x => x.Id == id)
              .Include(x => x.User)
              .FirstAsync();
        }

        public async Task<Teacher> GetByUserIdAsync(Guid userId)
        {
            return await Context.Set<Teacher>()
              .Where(x => x.User.Id == userId)
              .Include(x => x.User)
              .FirstAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var teacherToBeDeleted = await GetTeacherAsync(id);
            var userToBeDeleted = teacherToBeDeleted.User;
            Context.Set<Teacher>().Remove(teacherToBeDeleted);
            Context.Set<ApplicationUser>().Remove(userToBeDeleted);
        }

        public async Task<IEnumerable<Teacher>> GetAllWithRelatedEntitiesAsync()
        {
            return await Context.Set<Teacher>()
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetByDepartmentAsync(Department department)
        {
            return await Context.Set<Teacher>()
                .Where(x => x.Department == department)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
