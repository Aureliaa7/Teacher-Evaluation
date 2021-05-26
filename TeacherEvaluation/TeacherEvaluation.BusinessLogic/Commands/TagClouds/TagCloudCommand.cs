using MediatR;
using Sparc.TagCloud;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.TagClouds
{
    public class TagCloudCommand : IRequest<IEnumerable<TagCloudTag>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
        public string TaughtSubjectId { get; set; }
    }
}
