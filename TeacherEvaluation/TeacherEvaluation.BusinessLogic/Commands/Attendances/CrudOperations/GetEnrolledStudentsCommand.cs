using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetEnrolledStudentsCommand : IRequest<IEnumerable<Student>>
    {
        public Guid SubjectId { get; set; }
        public Guid UserId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
