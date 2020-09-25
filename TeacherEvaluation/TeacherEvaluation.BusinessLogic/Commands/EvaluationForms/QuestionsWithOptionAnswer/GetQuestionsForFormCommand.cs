using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class GetQuestionsForFormCommand : IRequest<IEnumerable<QuestionWithOptionAnswer>>
    {
        public Guid FormId { get; set; }
    }
}
