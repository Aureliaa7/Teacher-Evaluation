using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Grades
{
    [Authorize(Roles ="Administrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [Required(ErrorMessage = "Student is required")]
        [Display(Name = "Student")]
        public Guid StudentId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        [Display(Name = "Subject")]
        public Guid SubjectId { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        [Display(Name = "Taught subject type")]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Grade is required")]
        [Range(1, 10, ErrorMessage ="The grade must be between 1 and 10")]
        [Display(Name = "Grade")]
        public int? Grade { get; set; } = null;

        [BindProperty]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date")]
        public DateTime? DateTime { get; set; }

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        [Display(Name = "Study programme")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        [Display(Name = "Study domain")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Specialization is required")]
        public Guid SpecializationId { get; set; }

        [BindProperty]
        [Display(Name = "Study year")]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                UpdateGradeCommand command = new UpdateGradeCommand
                {
                    SubjectId = SubjectId,
                    StudentId = StudentId,
                    Type = Type,
                    Value = (int)Grade,
                    Date = (DateTime)DateTime
                };
                try
                {
                    await mediator.Send(command);
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }
                return RedirectToPage("../Students/Index");
            }
            return Page();
        }
    }
}
