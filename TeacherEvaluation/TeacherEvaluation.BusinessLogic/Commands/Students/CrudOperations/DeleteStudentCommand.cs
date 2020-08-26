using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class DeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
