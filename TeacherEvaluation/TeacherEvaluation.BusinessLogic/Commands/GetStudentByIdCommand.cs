using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class GetStudentByIdCommand : IRequest<Student>
    {
        public Guid Id { get; set; }
    }
}
