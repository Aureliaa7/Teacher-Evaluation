using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class IndexModel : PageModel
    {
        private readonly TeacherEvaluation.DataAccess.Data.ApplicationDbContext _context;

        public IndexModel(TeacherEvaluation.DataAccess.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TaughtSubject> TaughtSubject { get;set; }

        public async Task OnGetAsync()
        {
            TaughtSubject = await _context.TaughtSubjects.ToListAsync();
        }
    }
}
