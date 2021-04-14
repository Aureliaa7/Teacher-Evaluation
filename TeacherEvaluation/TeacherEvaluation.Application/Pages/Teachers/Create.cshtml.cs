using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : TeacherBaseModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<string> ErrorMessages { get; set; }

        public CreateModel(IMediator mediator) : base(mediator)
        {
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
                    ConfirmationUrlTemplate = confirmationUrlTemplate,
                    Password = Password
                };
                await mediator.Send(command);
                return RedirectToPage("../Teachers/Index");
            }
            return Page();
        }
    }
}