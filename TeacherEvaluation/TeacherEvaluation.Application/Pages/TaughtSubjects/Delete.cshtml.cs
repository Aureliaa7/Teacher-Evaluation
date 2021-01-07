using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid TaughtSubjectId { get; set; }

        [BindProperty]
        [Display(Name = "Teacher name")]
        public string TeacherName { get; set; }

        [BindProperty]
        [Display(Name = "Subject title")]
        public string SubjectTitle{ get; set; }

        [BindProperty]
        public TaughtSubjectType Type { get; set; }

        public DeleteModel(IMediator mediator)
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteTaughtSubjectCommand command = new DeleteTaughtSubjectCommand
                {
                    Id = TaughtSubjectId
                };
                await mediator.Send(command);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return RedirectToPage("../TaughtSubjects/Index");
        }
    }
}
