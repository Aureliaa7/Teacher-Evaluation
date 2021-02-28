using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Students
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

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "Study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Display(Name = "Study domain")]
        [Required(ErrorMessage = "Specialization is required")]
        public Guid SpecializationId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Group is required")]
        [RegularExpression(pattern: "^[a-zA-Z1-4\\.1-4a-zA-Z]+$", ErrorMessage = "Invalid text")]
        public string Group { get; set; }

        public List<string> ErrorMessages { get; set; }

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
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
                    Password = Password,
                    PIN = PIN,
                    Group = Group,
                    SpecializationId = SpecializationId,
                    StudyYear = (int)StudyYear,
                    ConfirmationUrlTemplate = confirmationUrlTemplate
                };
                await mediator.Send(command);
                return RedirectToPage("../Students/Index");
            }
            return Page();
        }
    }
}