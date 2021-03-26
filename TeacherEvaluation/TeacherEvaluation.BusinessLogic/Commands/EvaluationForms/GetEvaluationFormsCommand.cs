using MediatR;
using System.Collections.Generic;

using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetEvaluationFormsCommand : IRequest<IEnumerable<Form>>
    {
    }
}
