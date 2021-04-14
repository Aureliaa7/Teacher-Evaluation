using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class ChartsDataCommand : IRequest<IDictionary<string, IDictionary<string, int>>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
