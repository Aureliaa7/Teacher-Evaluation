using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : EnrollmentBaseModel
    {
        public DeleteModel(IMediator mediator): base(mediator)
        { 
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            EnrollmentId = (Guid)id;
            GetEnrollmentByIdCommand command = new GetEnrollmentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Enrollment enrollment = await mediator.Send(command);
                InitializeDetails(enrollment);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DeleteEnrollmentCommand command = new DeleteEnrollmentCommand { Id = (Guid)EnrollmentId };
            await mediator.Send(command);

            return RedirectToPage("./Index");
        }
    }
}
