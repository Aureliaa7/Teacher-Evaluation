using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectTypesByTeacherAndSubjectCommand : IRequest<List<TaughtSubjectType>>
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
