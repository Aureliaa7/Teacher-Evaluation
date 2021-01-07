using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Student")]
    public class DoneLaboratoriesModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<TaughtSubject> TaughtSubjects { get; set; }

        public DoneLaboratoriesModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetSubjectsForStudentCommand command = new GetSubjectsForStudentCommand { SubjectType = TaughtSubjectType.Laboratory, 
                UserId = currentUserId, EnrollmentState = EnrollmentState.Done };
            try
            {
                TaughtSubjects = await mediator.Send(command);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("/Errors/404");
            }
            return Page();
        }
    }
}