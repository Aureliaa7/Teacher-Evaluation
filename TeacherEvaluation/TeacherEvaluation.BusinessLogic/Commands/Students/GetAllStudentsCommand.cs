using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students
{
    public class GetAllStudentsCommand : IRequest<IEnumerable<Student>>
    {
    }
}
