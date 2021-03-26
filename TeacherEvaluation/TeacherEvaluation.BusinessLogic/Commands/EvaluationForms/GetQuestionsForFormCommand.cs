using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetQuestionsForFormCommand : IRequest<IEnumerable<Question>>
    {
        public Guid FormId { get; set; }
    }
}
