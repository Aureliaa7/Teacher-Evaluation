using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects
{
    public class DeleteSubjectCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
