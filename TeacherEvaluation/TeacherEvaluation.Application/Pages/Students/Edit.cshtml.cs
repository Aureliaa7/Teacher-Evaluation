using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        public Guid StudentId { get; set; }

        [BindProperty]
        public string Email { get; set; }

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
        [Required(ErrorMessage = "Specialization is required")]
        public Guid SpecializationId { get; set; }

        [BindProperty]
        public Specialization Specialization { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Group is required")]
        [RegularExpression(pattern: "^[a-zA-Z1-4\\.1-4a-zA-Z]+$", ErrorMessage = "Invalid text")]
        public string Group { get; set; }


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
            StudentId = (Guid)id;
            GetStudentByIdCommand command = new GetStudentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Student student = await mediator.Send(command);
                FirstName = student.User.FirstName;
                LastName = student.User.LastName;
                Email = student.User.Email;
                FathersInitial = student.User.FathersInitial;
                PIN = student.PIN;
                Group = student.Group;
                Specialization = student.Specialization;
                StudyDomainId = StudyDomainId;
                SpecializationId = student.Specialization.Id;
                StudyYear = student.StudyYear;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public IActionResult OnGetReturnStudyProgrammes()
        {
            var studyProgrammes = new SelectList(Enum.GetValues(typeof(StudyProgramme)).OfType<Enum>()
                                                                                       .Select(x => new SelectListItem
                                                                                       {
                                                                                           Text = Enum.GetName(typeof(StudyProgramme), x),
                                                                                           Value = (Convert.ToInt32(x)).ToString()
                                                                                       }), "Value", "Text");
            return new JsonResult(studyProgrammes);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateStudentCommand command = new UpdateStudentCommand
                    {
                        Id = StudentId,
                        Email = Email,
                        FathersInitial = FathersInitial,
                        FirstName = FirstName,
                        Group = Group,
                        LastName = LastName,
                        PIN = PIN,
                        SpecializationId = SpecializationId,
                        StudyYear = (int)StudyYear
                    };
                    await mediator.Send(command);
                    return RedirectToPage("../Students/Index");
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

