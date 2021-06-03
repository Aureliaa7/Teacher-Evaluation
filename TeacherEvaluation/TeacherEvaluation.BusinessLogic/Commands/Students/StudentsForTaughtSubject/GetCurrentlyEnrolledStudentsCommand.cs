using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.StudentsForTaughtSubject
{
    /// <summary>
    ///  Used to get the currently enrolled students based on the taught subject id
    /// </summary>
    public class GetCurrentlyEnrolledStudentsCommand : IRequest<IEnumerable<Student>>
    {
        public Guid TaughtSubjectId { get; set; }
    }
}
