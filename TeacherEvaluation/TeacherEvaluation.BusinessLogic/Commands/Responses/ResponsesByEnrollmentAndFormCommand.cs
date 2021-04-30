using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesByEnrollmentAndFormCommand : IRequest<IDictionary<string, string>>
    {
        public Guid FormId { get; set; }
        public Guid EnrollmentId { get; set; }
    }
}
