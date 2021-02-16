using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class CreateFormCommand : IRequest
    {
        public IEnumerable<string> Questions { get; set; }
        public FormType FormType { get; set; }
    }
}
