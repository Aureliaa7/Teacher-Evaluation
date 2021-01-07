using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
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

        [BindProperty]
        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        public List<SelectListItem> Subjects { get; set; }
        

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet()
        {
            GetAllSubjectsCommand getSubjectsCommand = new GetAllSubjectsCommand();
            var subjects = await mediator.Send(getSubjectsCommand);

            Subjects = subjects.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Name
                                            }).ToList();
            return Page();
        }

        public IActionResult OnGetReturnTeachersByDepartment(string department)
        {
            GetTeachersByDepartmentCommand command = new GetTeachersByDepartmentCommand { Department = (Department)Enum.Parse(typeof(Department), department) };
            var teachers = mediator.Send(command).Result;
            return new JsonResult(teachers);
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
