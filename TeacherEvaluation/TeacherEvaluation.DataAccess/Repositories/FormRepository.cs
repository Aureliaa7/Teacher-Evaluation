using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.DataAccess.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Form>> GetAllByType(FormType type)
        {
            return await Context.Set<Form>()
                .Where(x => x.Type == type)
                .ToListAsync();
        }

        public async Task<Form> GetByDate(DateTime currentDate)
        {
            return await Context.Set<Form>()
                .Where(x => x.StartDate <= currentDate && x.EndDate > currentDate)
                .FirstAsync();
        }
    }
}
