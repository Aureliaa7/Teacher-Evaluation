using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.DTOs;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetFormsByTypeCommand : IRequest<IEnumerable<FormDto>>
    {
    }
}
