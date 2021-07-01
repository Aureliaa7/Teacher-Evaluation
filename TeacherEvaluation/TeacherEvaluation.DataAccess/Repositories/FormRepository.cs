using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Form> GetByDateAsync(DateTime currentDate)
        {
            return await Context.Set<Form>()
                .FirstOrDefaultAsync(x => x.StartDate <= currentDate && x.EndDate > currentDate);
        }
    }
}
