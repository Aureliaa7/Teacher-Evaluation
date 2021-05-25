using MediatR;
using Sparc.TagCloud;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.TagClouds
{
    // TODO when creating the tag cloud, the taught subject id should also be taken into account
    public class TagCloudCommand : IRequest<IEnumerable<TagCloudTag>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
        public string TaughtSubjectId { get; set; }
    }
}
