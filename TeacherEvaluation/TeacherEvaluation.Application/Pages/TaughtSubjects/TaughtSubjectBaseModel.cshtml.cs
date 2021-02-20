using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class TaughtSubjectBaseModelModel : PageModel
    {
        public IEnumerable<TaughtSubject> TaughtSubjects { get; set; }
        public CurrentRole CurrentRole { get; set; }
    }
}
