using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Questions
{
    public class LikertQuestionsCommand : IRequest<IEnumerable<Question>>
    {
        public Guid FormId { get; set; }
    }
}
