using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<Teacher> Teachers { get; set; }

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
            Teachers = new List<Teacher>();
        }

        public async Task OnGetAsync()
        {
            GetAllTeachersCommand command = new GetAllTeachersCommand();
            Teachers = await mediator.Send(command);
        }

        public IActionResult OnGetReturnTeachersByDepartment(string department)
        {
            GetTeachersByDepartmentCommand command = new GetTeachersByDepartmentCommand { Department = (Department)Enum.Parse(typeof(Department), department) };
            var teachers = mediator.Send(command).Result;
            return new JsonResult(teachers);
        }

        public IActionResult OnGetReturnTeachersBySubjectAndType(string subjectId, string type)
        {
            GetTeachersForSubjectCommand command = new GetTeachersForSubjectCommand
            {
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            IEnumerable<Teacher> teachers = new List<Teacher>();
            teachers = mediator.Send(command).Result;
            return new JsonResult(teachers);
        }
    }
}
