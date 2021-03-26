using MediatR;
using System;
using System.Collections.Generic;

using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class CreateFormCommand : IRequest
    {
        public IEnumerable<string> Questions { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinNumberOfAttendances { get; set; }
    }
}
