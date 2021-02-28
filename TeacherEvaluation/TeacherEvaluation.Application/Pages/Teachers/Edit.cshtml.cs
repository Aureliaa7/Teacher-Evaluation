using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class EditModel : TeacherBaseModel
    {
        public EditModel(IMediator mediator): base(mediator)
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
                Teacher teacher = await mediator.Send(command);
                FirstName = teacher.User.FirstName;
                LastName = teacher.User.LastName;
                Email = teacher.User.Email;
                FathersInitial = teacher.User.FathersInitial;
                PIN = teacher.User.PIN;
                Degree = teacher.Degree;
                Department = teacher.Department;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateTeacherCommand command = new UpdateTeacherCommand
                    {
                        Id = TeacherId,
                        Email = Email,
                        FathersInitial = FathersInitial,
                        FirstName = FirstName,
                        LastName = LastName,
                        PIN = PIN,
                        Degree = Degree,
                        Department = Department
                    };
                    await mediator.Send(command);
                    return RedirectToPage("../Teachers/Index");
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
