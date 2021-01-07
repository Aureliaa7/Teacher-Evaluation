using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetTeachersForSubjectCommand : IRequest<IEnumerable<Teacher>>
    {
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
