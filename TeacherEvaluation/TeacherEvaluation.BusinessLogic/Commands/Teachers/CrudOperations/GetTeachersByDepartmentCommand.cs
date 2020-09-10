using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class GetTeachersByDepartmentCommand : IRequest<IEnumerable<Teacher>>
    {
        public Department Department { get; set; }
    }
}
