using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects
{
    public class DeleteTaughtSubjectCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
