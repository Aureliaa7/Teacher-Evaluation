using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : StudentBaseModel
    {
        public IndexModel(IMediator mediator): base(mediator)
        {
        }

        public async Task OnGetAsync()
        {
            GetAllStudentsCommand command = new GetAllStudentsCommand();
            Students = await mediator.Send(command);
            CurrentRole.IsAdmin = true;
        }

        public async Task<JsonResult> OnGetReturnStudents(string specializationId, string studyYear)
        {
            if (!string.IsNullOrEmpty(specializationId) && !string.IsNullOrEmpty(studyYear))
            {
                try
                {
                    GetStudentsBySpecializationIdAndYearCommand command = new GetStudentsBySpecializationIdAndYearCommand
                    {
                        SpecializationId = new Guid(specializationId),
                        StudyYear = int.Parse(studyYear)
                    };
                    var students = await mediator.Send(command);
                    return new JsonResult(students);
                }
                catch(ItemNotFoundException) { }
            }
            return new JsonResult("");
        }
    }
}
