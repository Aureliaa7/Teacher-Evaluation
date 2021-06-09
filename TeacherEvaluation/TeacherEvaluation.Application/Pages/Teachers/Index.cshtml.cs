using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
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
            var teachers = await mediator.Send(command);
            Teachers = teachers.OrderBy(t => t.User.FirstName);
        }

        public async Task<JsonResult> OnGetReturnTeachersByDepartment(string department)
        {
            if (!string.IsNullOrEmpty(department))
            {
                try
                {
                    GetTeachersByDepartmentCommand command = new GetTeachersByDepartmentCommand { Department = (Department)Enum.Parse(typeof(Department), department) };
                    var teachers = await mediator.Send(command);
                    teachers = teachers.OrderBy(t => t.User.FirstName);

                    return new JsonResult(teachers);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult(new List<Teacher>());
        }

        public async Task<JsonResult> OnGetReturnTeachersBySubjectAndType(string subjectId, string type)
        {
            if (!string.IsNullOrEmpty(subjectId) && !string.IsNullOrEmpty(type))
            {
                try
                {
                    GetTeachersForSubjectCommand getTeachersCommand = new GetTeachersForSubjectCommand
                    {
                        SubjectId = new Guid(subjectId),
                        Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
                    };
                    var teachers = await mediator.Send(getTeachersCommand);
                    teachers = teachers.OrderBy(t => t.User.FirstName);

                    return new JsonResult(teachers);
                }
                catch (ItemNotFoundException) { }
            }
            return new JsonResult(new List<Teacher>());
        }
    }
}
