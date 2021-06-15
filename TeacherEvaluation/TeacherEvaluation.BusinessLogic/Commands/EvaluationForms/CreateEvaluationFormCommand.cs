using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class CreateEvaluationFormCommand : IRequest
    {
        public IEnumerable<string> Questions { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int MinNumberOfAttendances { get; init; }
        public Semester Semester { get; init; }

    }
}
