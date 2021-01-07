using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeacherByCriteriaCommand : IRequest<Teacher>
    {
        public Guid SubjectId { get; set; }
        public Guid UserIdForStudent { get; set; }
        public TaughtSubjectType SubjectType { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
    }
}
