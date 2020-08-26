using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetAllEnrollmentsCommand : IRequest<IEnumerable<Enrollment>>
    {
    }
}
