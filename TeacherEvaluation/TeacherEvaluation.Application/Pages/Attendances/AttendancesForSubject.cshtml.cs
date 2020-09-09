using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Attendances
{
    [Authorize]
    public class AttendancesForSubjectModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<Attendance> Attendances { get; set; }

        public AttendancesForSubjectModel(IMediator mediator)
        {
            this.mediator = mediator;
            Attendances = new List<Attendance>();
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if(id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            else
            {
                Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                GetAttendancesForSubjectCommand command = new GetAttendancesForSubjectCommand { TaughtSubjectId = (Guid)id, UserId = currentUserId };
                try
                {
                    Attendances = await mediator.Send(command);
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }
            }
            return Page();
        }
    }
}