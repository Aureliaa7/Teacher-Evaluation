using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Teachers;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Personal identification number is required")]
        [RegularExpression(pattern: "^(?!(.)\\1{3})(?!19|20)\\d{13}$", ErrorMessage = "Invalid text")]
        [MaxLength(13)]
        public string PIN { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+$", ErrorMessage = "Invalid text")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+$", ErrorMessage = "Invalid text")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Father's initial is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+(.)+$", ErrorMessage = "Invalid text")]
        [MaxLength(2)]
        public string FathersInitial { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Degree is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+$", ErrorMessage = "Invalid text")]
        public string Degree { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Department is required")]
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        public EditModel(IMediator mediator)
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
                Teacher teacher = await mediator.Send(command);
                FirstName = teacher.User.FirstName;
                LastName = teacher.User.LastName;
                Email = teacher.User.Email;
                FathersInitial = teacher.User.FathersInitial;
                PIN = teacher.PIN;
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
