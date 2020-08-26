using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances
{
    public class AddAttendanceCommand : IRequest
    {
        public TaughtSubjectType Type { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
