using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class UpdateNoAttendancesCommand : IRequest
    {
        public TaughtSubjectType Type { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfAttendances { get; set; }
    }
}
