using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class DeleteModel : TeacherBaseModel
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
            TeacherId = (Guid)id;
            GetTeacherByIdCommand command = new GetTeacherByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Teacher teacherToBeDeleted = await mediator.Send(command);
                FirstName = teacherToBeDeleted.User.FirstName;
                LastName = teacherToBeDeleted.User.LastName;
                Email = teacherToBeDeleted.User.Email;
                FathersInitial = teacherToBeDeleted.User.FathersInitial;
                PIN = teacherToBeDeleted.User.PIN;
                Department = teacherToBeDeleted.Department;
                Degree = teacherToBeDeleted.Degree;
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
                DeleteTeacherCommand command = new DeleteTeacherCommand
                {
                    Id = TeacherId
                };
                await mediator.Send(command);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return RedirectToPage("../Teachers/Index");
        }
    }
}
