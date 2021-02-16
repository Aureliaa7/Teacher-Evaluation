using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class EditFormCommand : IRequest
    {
        public Guid FormId { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
        public int MinNumberAttendances { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
