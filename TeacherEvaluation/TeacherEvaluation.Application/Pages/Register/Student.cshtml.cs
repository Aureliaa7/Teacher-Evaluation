using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Students;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Register
{
    [Authorize(Roles="Administrator")]
    public class StudentModel : PageModel
    {
        private readonly IMediator mediator;

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
        public int? StudyYear { get; set; } = null;


        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Section is required")]
        [RegularExpression(pattern: "^[a-zA-Z-]+$", ErrorMessage = "Invalid text")]
        public string Section { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Group is required")]
        [RegularExpression(pattern: "^[a-zA-Z1-4\\.1-4a-zA-Z]+$", ErrorMessage = "Invalid text")]
        public string Group { get; set; }

        public List<string> ErrorMessages { get; set; }

        public StudentModel(IMediator mediator)
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
                StudentRegistrationCommand command = new StudentRegistrationCommand
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    FathersInitial = FathersInitial,
                    Password = Password,
                    PIN = PIN,
                    Group = Group,
                    Section = Section,
                    StudyProgramme = StudyProgramme,
                    StudyYear = (int)StudyYear
                };
                await mediator.Send(command);
                return RedirectToPage("../Students/Index");
            }
            return Page();
        }
    }
}