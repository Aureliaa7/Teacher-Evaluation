using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetUserIdForStudentCommand : IRequest<Guid>
    {
        public Guid StudentId { get; set; }
    }
}
