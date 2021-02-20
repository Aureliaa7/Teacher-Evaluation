using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Student")]
    public class InProgressLaboratoriesModel : TaughtSubjectBaseModelModel
    {
        private readonly IMediator mediator;

        public InProgressLaboratoriesModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetSubjectsForStudentCommand command = new GetSubjectsForStudentCommand { SubjectType = TaughtSubjectType.Laboratory, 
                UserId = currentUserId, EnrollmentState = EnrollmentState.InProgress};
            try
            {
                TaughtSubjects = await mediator.Send(command);
                CurrentRole = new CurrentRole();
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
