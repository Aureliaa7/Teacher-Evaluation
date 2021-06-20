using MediatR;
using Sparc.TagCloud;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.TagClouds
{
    public class TagCloudCommand : IRequest<IEnumerable<TagCloudTag>>
    {
        public EvaluationFormResponseRetrievalCriteria ResponseRetrievalCriteria { get; set; }
    }
}
