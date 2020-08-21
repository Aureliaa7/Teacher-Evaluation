using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments
{
    public class GetEnrollmentsForStudentCommand : IRequest<IEnumerable<Enrollment>>
    {
        public Guid Id { get; set; }
    }
}
