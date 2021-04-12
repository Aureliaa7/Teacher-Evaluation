using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommand : IRequest<IEnumerable<IDictionary<string, string>>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
