using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommand : IRequest<IDictionary<string, ResponseVm>>
    {
        public EvaluationFormResponseRetrievalCriteria ResponseRetrievalCriteria { get; set; }
    }
}
