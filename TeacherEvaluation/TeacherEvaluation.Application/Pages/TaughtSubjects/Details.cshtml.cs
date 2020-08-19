using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid TaughtSubjectId { get; set; }

        [BindProperty]
        [Display(Name = "Taught subject type")]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Display(Name = "Teacher name")]
        public string TeacherName { get; set; }

        [BindProperty]
        [Display(Name = "Subject title")]
        public string SubjectTitle { get; set; }

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            TaughtSubjectId = (Guid)id;
            GetTaughtSubjectByIdCommand command = new GetTaughtSubjectByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                TaughtSubject taughtSubjectToBeDeleted = await mediator.Send(command);
                TeacherName = taughtSubjectToBeDeleted.Teacher.User.FirstName + " " + taughtSubjectToBeDeleted.Teacher.User.LastName;
                SubjectTitle = taughtSubjectToBeDeleted.Subject.Name;
                Type = taughtSubjectToBeDeleted.Type;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}
