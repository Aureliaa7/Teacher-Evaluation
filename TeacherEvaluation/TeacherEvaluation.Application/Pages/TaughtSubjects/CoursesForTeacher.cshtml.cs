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
    [Authorize(Roles = "Teacher")]
    public class CoursesForTeacherModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<TaughtSubject> TaughtSubjects { get; set; }

        public CoursesForTeacherModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                GetTaughtSubjectsByTypeCommand command = new GetTaughtSubjectsByTypeCommand { UserId = currentUserId, Type = TaughtSubjectType.Course };
                TaughtSubjects = await mediator.Send(command);
            }catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}