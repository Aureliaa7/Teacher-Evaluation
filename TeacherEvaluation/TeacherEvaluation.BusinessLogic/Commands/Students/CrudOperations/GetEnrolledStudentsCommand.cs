using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetEnrolledStudentsCommand : IRequest<IEnumerable<Student>>
    {
        public Guid SubjectId { get; set; }
        public Guid UserId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
