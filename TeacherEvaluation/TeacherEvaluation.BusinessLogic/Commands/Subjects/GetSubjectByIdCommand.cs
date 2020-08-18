using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects
{
    public class GetSubjectByIdCommand : IRequest<Subject>
    {
        public Guid Id { get; set; }
    }
}
