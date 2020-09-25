using MediatR;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class GetEvaluationFormCommand : IRequest<Form>
    {
    }
}
