﻿using MediatR;
using System;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommand : IRequest<IDictionary<string, Guid>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
        public string TaughtSubjectId { get; set; }
    }
}
