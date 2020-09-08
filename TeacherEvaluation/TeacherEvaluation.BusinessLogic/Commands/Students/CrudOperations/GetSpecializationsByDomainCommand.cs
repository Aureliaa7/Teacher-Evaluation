using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetSpecializationsByDomainCommand : IRequest<IEnumerable<Specialization>>
    {
        public Guid StudyDomainId { get; set; }
    }
}
