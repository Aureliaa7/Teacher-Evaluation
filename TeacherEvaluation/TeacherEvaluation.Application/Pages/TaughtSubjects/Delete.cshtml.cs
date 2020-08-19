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
    public class DeleteModel : PageModel
    {
        private readonly TeacherEvaluation.DataAccess.Data.ApplicationDbContext _context;

        public DeleteModel(TeacherEvaluation.DataAccess.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaughtSubject TaughtSubject { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaughtSubject = await _context.TaughtSubjects.FirstOrDefaultAsync(m => m.Id == id);

            if (TaughtSubject == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaughtSubject = await _context.TaughtSubjects.FindAsync(id);

            if (TaughtSubject != null)
            {
                _context.TaughtSubjects.Remove(TaughtSubject);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
