using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : TaughtSubjectBaseModel
    {
        public IndexModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGetAsync()
        {
            CurrentRole.IsAdmin = true;
            GetAllTaughtSubjectsCommand command = new GetAllTaughtSubjectsCommand();
            TaughtSubjects = await mediator.Send(command);
        }

        public async Task<JsonResult> OnGetReturnAssignedSubjectsByTeacherId(string teacherId)
        {
            if(!string.IsNullOrEmpty(teacherId))
            {
                var command = new GetAssignedSubjectsByTeacherIdCommand
                {
                    TeacherId = new Guid(teacherId)
                };
                var subjectAndIds = await mediator.Send(command);
                return new JsonResult(subjectAndIds);
            }
            return new JsonResult("");
        }
    }
}
