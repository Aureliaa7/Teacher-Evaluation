using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class GetAllSubjectsCommand : IRequest<IEnumerable<Subject>>
    {
    }
}
