using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetAllTaughtSubjectsCommand : IRequest<IEnumerable<TaughtSubject>>
    {
    }
}
