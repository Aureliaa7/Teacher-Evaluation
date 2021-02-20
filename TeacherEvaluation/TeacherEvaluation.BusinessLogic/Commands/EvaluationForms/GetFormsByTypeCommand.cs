using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetFormsByTypeCommand : IRequest<IEnumerable<Form>>
    {
        public FormType Type { get; set; } 
    }
}
