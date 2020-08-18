using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class GetTeacherByIdCommand : IRequest<Teacher>
    {
        public Guid Id { get; set; }
    }
}
