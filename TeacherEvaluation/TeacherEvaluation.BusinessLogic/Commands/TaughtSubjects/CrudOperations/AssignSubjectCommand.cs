using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class AssignSubjectCommand : IRequest
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
        public int MaxNumberOfAttendances { get; set; }
    }
}
