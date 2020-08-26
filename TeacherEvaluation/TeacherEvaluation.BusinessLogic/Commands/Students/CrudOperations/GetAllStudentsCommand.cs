using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetAllStudentsCommand : IRequest<IEnumerable<Student>>
    {
    }
}
