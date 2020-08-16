using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class UpdateSubjectCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfCredits { get; set; }
    }
}
