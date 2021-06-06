using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Grades
{
    [Authorize(Roles = "Administrator")]
    public class SearchByCriteriaModel : PageModel
    {
        [BindProperty]
        [Required]
        public Department Department { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Teacher")]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Subject")]
        public Guid SubjectId { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Type")]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "From year")]
        public int? FromYear { get; set; } = null;

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync()
        {
            return RedirectToPage("../Grades/Index", new
            {
                subjectId = SubjectId,
                teacherId = TeacherId,
                type = Type,
                year = FromYear
            });
        }
    }
}
