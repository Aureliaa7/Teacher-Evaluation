using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherIdByUserIdCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
