using MediatR;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Charts
{
    public class ChartsDataCommand : IRequest<IDictionary<string, IDictionary<string, int>>>
    {
        public EvaluationFormResponseRetrievalCriteria ResponseRetrievalCriteria { get; set; }
    }
}
