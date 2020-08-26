using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations
{
    public class GetSubjectsForTeacherCommand : IRequest<IEnumerable<TaughtSubject>>
    {
        public Guid UserId { get; set; }
    }
}
