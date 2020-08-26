using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class EnrollmentExistsCommand : IRequest<bool>
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
