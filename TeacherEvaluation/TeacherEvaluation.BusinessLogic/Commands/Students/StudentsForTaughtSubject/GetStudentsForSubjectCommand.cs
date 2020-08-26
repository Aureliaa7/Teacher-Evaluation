using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject
{
    public class GetStudentsForSubjectCommand : IRequest<IEnumerable<Student>>
    {
        public Guid TaughtSubjectId { get; set; }
    }
}
