using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    /// <summary>
    /// This is used to get the assigned subjects to a teacher in order to view grades by criteria
    /// </summary>
    public class GetAssignedSubjectsByTeacherIdCommand : IRequest<IDictionary<string, string>>
    {
        public Guid TeacherId { get; init; }
    }
}
