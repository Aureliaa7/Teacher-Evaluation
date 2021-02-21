using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Teacher")]
    public class CoursesForTeacherModel : TaughtSubjectBaseModel
    {
        public CoursesForTeacherModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                GetTaughtSubjectsByTypeCommand command = new GetTaughtSubjectsByTypeCommand { UserId = currentUserId, Type = TaughtSubjectType.Course };
                TaughtSubjects = await mediator.Send(command);
                CurrentRole.IsTeacher = true;
            }catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}