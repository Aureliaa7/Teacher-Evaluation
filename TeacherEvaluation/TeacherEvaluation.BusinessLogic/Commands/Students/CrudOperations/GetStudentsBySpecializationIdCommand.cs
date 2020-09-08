using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentsBySpecializationIdCommand : IRequest<IEnumerable<Student>>
    {
        public Guid SpecializationId { get; set; } 
    }
}
