using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetSubjectsForEnrollmentsBySemesterCommand : IRequest<IEnumerable<Subject>>
    {
        public Guid UserId { get; set; }
        public Semester Semester { get; set; }
    }
}
