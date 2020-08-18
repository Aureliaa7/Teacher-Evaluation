using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class DeleteTeacherCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
