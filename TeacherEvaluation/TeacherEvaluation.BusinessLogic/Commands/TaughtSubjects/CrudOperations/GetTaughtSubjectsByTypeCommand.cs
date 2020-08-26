using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectsByTypeCommand : IRequest<IEnumerable<TaughtSubject>>
    {
        public Guid UserId { get; set; }
        public TaughtSubjectType Type { get; set; }
    }
}
