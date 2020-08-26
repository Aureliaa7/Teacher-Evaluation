using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Students
{
    public class EnrolledStudentsModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<Student> EnrolledStudents { get; set; }

        public EnrolledStudentsModel(IMediator mediator)
        {
            this.mediator = mediator;
            EnrolledStudents = new List<Student>();
        }

        public async Task OnGet(Guid? id)
        {
            GetStudentsForSubjectCommand command = new GetStudentsForSubjectCommand { TaughtSubjectId = (Guid)id };
            EnrolledStudents = await mediator.Send(command);
        }
    }
}