using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [Required(ErrorMessage = "Personal identification number is required")]
        [RegularExpression(pattern: "[1-9]([0-9]{12}$)", ErrorMessage = "Invalid text")]
        [MaxLength(13)]
        public string PIN { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Degree is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(2)]
        public string Degree { get; set; }

        [BindProperty]
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        [MinLength(3)]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Father's initial is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+(.)+$", ErrorMessage = "Invalid text")]
        [MaxLength(2)]
        public string FathersInitial { get; set; }

        [BindProperty]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public List<string> ErrorMessages { get; set; }

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string confirmationUrlTemplate = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { id = "((userId))", token = "((token))" },
                        protocol: Request.Scheme);

                TeacherRegistrationCommand command = new TeacherRegistrationCommand
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Department = Department,
                    Degree = Degree,
                    Email = Email,
                    FathersInitial = FathersInitial,
                    PIN = PIN,
                    ConfirmationUrlTemplate = confirmationUrlTemplate
                };
                await mediator.Send(command);
                return RedirectToPage("../Teachers/Index");
            }
            return Page();
        }
    }
}