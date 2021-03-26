using MediatR;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetEvaluationFormCommand : IRequest<Form>
    {
    }
}
