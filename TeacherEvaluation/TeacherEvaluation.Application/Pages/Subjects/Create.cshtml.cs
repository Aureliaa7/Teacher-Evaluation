using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : SubjectBaseModel
    {
        public CreateModel(IMediator mediator): base(mediator)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelIsValid())
            {
                AddSubjectCommand command = new AddSubjectCommand
                {
                    Name = SubjectName,
                    NumberOfCredits = (int)NumberOfCredits,
                    StudyYear = (int) StudyYear,
                    SpecializationId = SpecializationId,
                    Semester = Semester
                };
                try
                {
                    await mediator.Send(command);
                    return RedirectToPage("/Subjects/Index");
                }
                catch(ItemNotFoundException)
                {
                    return RedirectToPage("/Errors/404");
                }
            }
            return Page();
        }

        private bool ModelIsValid()
        {
            return (SubjectName != null && NumberOfCredits != null && StudyYear != null && SpecializationId != null);
        }
    }
}
