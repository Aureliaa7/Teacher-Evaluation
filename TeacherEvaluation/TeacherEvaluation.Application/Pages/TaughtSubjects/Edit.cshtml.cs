﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeacherEvaluation.DataAccess.Data;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class EditModel : PageModel
    {
        private readonly TeacherEvaluation.DataAccess.Data.ApplicationDbContext _context;

        public EditModel(TeacherEvaluation.DataAccess.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TaughtSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaughtSubjectExists(TaughtSubject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TaughtSubjectExists(Guid id)
        {
            return _context.TaughtSubjects.Any(e => e.Id == id);
        }
    }
}
