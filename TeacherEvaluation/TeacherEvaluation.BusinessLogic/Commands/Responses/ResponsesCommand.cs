using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Responses
{
    public class ResponsesCommand : IRequest<IDictionary<string, ResponseVm>>
    {
        public Guid FormId { get; set; }
        public Guid TeacherId { get; set; }
        public string TaughtSubjectId { get; set; }
    }
}
