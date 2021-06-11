using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Charts
{
    public class ChartsDataCommand : IRequest<IDictionary<string, IDictionary<string, int>>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
        public string TaughtSubjectId { get; set; }
    }
}
