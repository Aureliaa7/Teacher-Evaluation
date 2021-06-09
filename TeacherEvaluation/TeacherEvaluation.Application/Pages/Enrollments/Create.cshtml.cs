using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : EnrollmentBaseModel
    {
        public List<SelectListItem> Subjects { get; set; }

        public CreateModel(IMediator mediator): base(mediator)
        {
        }

        public async Task<IActionResult> OnGet()
        { 
            GetAllSubjectsCommand getSubjectsCommand = new GetAllSubjectsCommand();
            var subjects = await mediator.Send(getSubjectsCommand);
            subjects = subjects.OrderBy(x => x.Name);
            Subjects = subjects.Select(x =>
                                            new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Name
                                            }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelIsValid())
            {
                EnrollStudentCommand command = new EnrollStudentCommand
                {
                    TeacherId = (Guid)TeacherId,
                    SubjectId = (Guid)SubjectId,
                    StudentId = (Guid)StudentId,
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

                return RedirectToPage("../Enrollments/Index");
            }
            return Page();
        }

        private bool ModelIsValid()
        {
            return (TeacherId != null && SubjectId != null && StudentId != null && Type != null);
        }
    }
}
