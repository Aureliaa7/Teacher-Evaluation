using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class CheckEnrollmentAvailabilityCommand : IRequest<bool>
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
