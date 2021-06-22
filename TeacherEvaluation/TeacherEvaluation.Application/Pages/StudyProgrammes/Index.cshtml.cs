using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.StudyProgrammes
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var studyProgrammes = new SelectList(Enum.GetValues(typeof(StudyProgramme)).OfType<Enum>()
                                                                                     .Select(x => new SelectListItem
                                                                                     {
                                                                                         Text = Enum.GetName(typeof(StudyProgramme), x),
                                                                                         Value = (Convert.ToInt32(x)).ToString()
                                                                                     }), "Value", "Text");
            return new JsonResult(studyProgrammes.OrderBy(x => x.Text));
        }
    }
}
