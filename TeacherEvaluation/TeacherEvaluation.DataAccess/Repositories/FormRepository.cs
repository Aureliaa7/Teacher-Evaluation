using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Form> GetByDate(DateTime currentDate)
        {
            return await Context.Set<Form>()
                .Where(x => x.StartDate <= currentDate && x.EndDate > currentDate)
                .FirstAsync();
        }
    }
}
