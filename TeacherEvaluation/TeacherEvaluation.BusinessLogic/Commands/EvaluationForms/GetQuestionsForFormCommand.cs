using MediatR;
using System;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class GetQuestionsForFormCommand : IRequest<QuestionsVm>
    {
        public Guid FormId { get; set; }
    }
}
