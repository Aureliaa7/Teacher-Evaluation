using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Students
{
    public class DeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
