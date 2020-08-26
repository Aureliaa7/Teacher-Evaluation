using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class DeleteSubjectCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
