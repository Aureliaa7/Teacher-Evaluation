using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class DeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
