using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : TaughtSubjectBaseModel
    {
        public List<SelectListItem> Subjects { get; set; }

        public CreateModel(IMediator mediator) : base(mediator)
        {
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

        public IActionResult OnGetUpdateSubjectAssignmentInfoField(string teacherId, string subjectId, string type)
        {
            if (!string.IsNullOrEmpty(teacherId) && !string.IsNullOrEmpty(subjectId) && !string.IsNullOrEmpty(type))
            {
                try
                {
                    AssignedSubjectVerificationCommand command = new AssignedSubjectVerificationCommand
                    {
                        TeacherId = new Guid(teacherId),
                        SubjectId = new Guid(subjectId),
                        Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
                    };
                    bool assignmentExists = mediator.Send(command).Result;
                    if (assignmentExists)
                    {
                        return new JsonResult("This assignment already exists");
                    }
                    return new JsonResult("This assignment is available");
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult("");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelIsValid())
            {
                AssignSubjectCommand command = new AssignSubjectCommand
                {
                    TeacherId = (Guid)TeacherId,
                    SubjectId = (Guid)SubjectId,
                    Type = (TaughtSubjectType)Type
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

        private bool ModelIsValid()
        {
            return (TeacherId != null && SubjectId != null && Type != null);
        }
    }
}
