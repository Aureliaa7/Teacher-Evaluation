using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetAttendancesForSubjectCommand : IRequest<int>
    {
        public Guid TaughtSubjectId { get; set; }
        public Guid UserId { get; set; }
    }
}
