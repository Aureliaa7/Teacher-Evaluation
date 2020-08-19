using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Subjects;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects;
using TeacherEvaluation.BusinessLogic.Commands.Teachers;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Teacher is required")]
        public Guid TeacherId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid SubjectId { get; set; }

        public List<SelectListItem> Teachers { get; set; }

        public List<SelectListItem> Subjects { get; set; }
        

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        {
            GetAllTeachersCommand getTeachersCommand = new GetAllTeachersCommand();
            var teachers = await mediator.Send(getTeachersCommand);

            GetAllSubjectsCommand getSubjectsCommand = new GetAllSubjectsCommand();
            var subjects = await mediator.Send(getSubjectsCommand);

            Teachers = teachers.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.User.FirstName + " " + x.User.LastName
                                            }).ToList();

            Subjects = subjects.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Name
                                            }).ToList();
            return Page();
        }

        public IActionResult OnGetUpdateInfoField(string teacherId, string subjectId, string type)
        {
            AssignedSubjectVerificationCommand command = new AssignedSubjectVerificationCommand
            {
                TeacherId = new Guid(teacherId),
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            bool assignmentExists = mediator.Send(command).Result;
            if(assignmentExists)
            {
                return new JsonResult("This assignment already exists");
            }
            return new JsonResult("This assignment is available");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                AssignSubjectCommand command = new AssignSubjectCommand
                {
                    TeacherId = TeacherId,
                    SubjectId = SubjectId,
                    Type = Type
                };
                try
                {
                    await mediator.Send(command);
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }

                return RedirectToPage("../TaughtSubjects/Index");
            }
            return Page();
        }
    }
}
