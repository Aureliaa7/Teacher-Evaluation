using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForEnrollmentsCommand : IRequest<IEnumerable<Subject>>
    {
        public Guid UserId { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
    }
}
