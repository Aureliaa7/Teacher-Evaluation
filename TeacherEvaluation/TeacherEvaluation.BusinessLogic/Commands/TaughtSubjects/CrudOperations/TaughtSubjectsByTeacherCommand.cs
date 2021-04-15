using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    /// <summary>
    /// This command provides the taught subject ids and their titles based
    /// on the teacher id and it is used for creating the second drop down from the ViewCharts page.
    /// </summary>
    public class TaughtSubjectsByTeacherCommand : IRequest<IDictionary<string, string>>
    {
        public Guid TeacherId { get; set; }
    }
}
