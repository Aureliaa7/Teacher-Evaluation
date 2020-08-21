using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments
{
    public class GetAllEnrollmentsCommand : IRequest<IEnumerable<Enrollment>>
    {
    }
}
