using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : StudentBaseModel
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

        public IActionResult OnGet()
        {
            return Page();
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

                StudentRegistrationCommand command = new StudentRegistrationCommand
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    FathersInitial = FathersInitial,
                    PIN = PIN,
                    Group = Group,
                    SpecializationId = (Guid)SpecializationId,
                    StudyYear = (int)StudyYear,
                    ConfirmationUrlTemplate = confirmationUrlTemplate,
                    Password = Password
                };
                await mediator.Send(command);
                return RedirectToPage("../Students/Index");
            }
            return Page();
        }
    }
}