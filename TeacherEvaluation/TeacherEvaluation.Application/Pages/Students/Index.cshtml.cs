using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;

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

        public IActionResult OnGetReturnStudents(string specializationId, string studyYear)
        {
            GetStudentsBySpecializationIdAndYearCommand command = new GetStudentsBySpecializationIdAndYearCommand
            {
                SpecializationId = new Guid(specializationId),
                StudyYear = int.Parse(studyYear)
            };
            var students = mediator.Send(command).Result;
            return new JsonResult(students);
        }
    }
}
