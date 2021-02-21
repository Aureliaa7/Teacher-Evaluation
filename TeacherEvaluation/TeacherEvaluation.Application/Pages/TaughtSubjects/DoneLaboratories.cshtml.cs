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
    [Authorize(Roles = "Student")]
    public class DoneLaboratoriesModel : TaughtSubjectBaseModel
    {
        public DoneLaboratoriesModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetSubjectsForStudentCommand command = new GetSubjectsForStudentCommand { SubjectType = TaughtSubjectType.Laboratory, 
                UserId = currentUserId, EnrollmentState = EnrollmentState.Done };
            try
            {
                TaughtSubjects = await mediator.Send(command);
                CurrentRole.IsStudent = true;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("/Errors/404");
            }
            return Page();
        }
    }
}