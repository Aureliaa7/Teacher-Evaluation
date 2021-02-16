using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [BindProperty]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [BindProperty]
        [Display(Name = "Father's initial")]
        public string FathersInitial { get; set; }

        [BindProperty]
        public string Degree { get; set; }

        [BindProperty]
        public Department Department { get; set; }

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
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
                Degree = teacherToBeDeleted.Degree;
                Department = teacherToBeDeleted.Department;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}
