using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class GetAllSubjectsCommand : IRequest<IEnumerable<Subject>>
    {
    }
}
