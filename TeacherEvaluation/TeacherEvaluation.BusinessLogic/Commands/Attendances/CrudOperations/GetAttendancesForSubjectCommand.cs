using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetAttendancesForSubjectCommand : IRequest<IEnumerable<Attendance>>
    {
        public Guid TaughtSubjectId { get; set; } 
        public Guid UserId { get; set; }
    }
}
