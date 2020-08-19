using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        { 
        }

        public async Task<Student> GetStudent(Guid id)
        {
            return await Context.Set<Student>()
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .FirstAsync();
        }

        public async Task Delete(Guid id)
        { 
            var studentToBeDeleted = await GetStudent(id);
            var userToBeDeleted = studentToBeDeleted.User;
            Context.Set<Student>().Remove(studentToBeDeleted);
            Context.Set<ApplicationUser>().Remove(userToBeDeleted);

            await Context.SaveChangesAsync();
        }
    }
}
