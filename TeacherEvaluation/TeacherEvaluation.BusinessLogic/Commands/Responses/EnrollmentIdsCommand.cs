using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class EnrollmentIdsCommand : IRequest<IEnumerable<Guid>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
