using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class CreateFormForQuestionWithOptionCommand : IRequest
    {
        public IEnumerable<string> Questions { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
        public int MinNumberAttendances { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
