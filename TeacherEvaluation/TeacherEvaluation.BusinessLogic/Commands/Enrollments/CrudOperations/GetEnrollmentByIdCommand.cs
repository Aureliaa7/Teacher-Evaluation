using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetEnrollmentByIdCommand : IRequest<Enrollment>
    {
        public Guid Id { get; set; }
    }
}
