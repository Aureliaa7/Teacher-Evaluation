using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments
{
    public class GetEnrollmentByIdCommand : IRequest<Enrollment>
    {
        public Guid Id { get; set; }
    }
}
