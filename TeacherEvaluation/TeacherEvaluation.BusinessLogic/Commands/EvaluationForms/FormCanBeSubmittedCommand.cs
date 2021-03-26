using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class FormCanBeSubmittedCommand : IRequest<bool>
    {
        public Guid SubjectId { get; set; }
        public Guid UserIdForStudent { get; set; }
        public Guid FormId { get; set; }
        public TaughtSubjectType SubjectType { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
    }
}
