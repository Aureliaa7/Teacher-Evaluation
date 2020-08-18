using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers
{
    public class GetAllTeachersCommand : IRequest<IEnumerable<Teacher>>
    {
    }
}
