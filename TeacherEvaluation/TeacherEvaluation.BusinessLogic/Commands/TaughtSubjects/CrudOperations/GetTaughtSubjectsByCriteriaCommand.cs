using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class GetTaughtSubjectsByCriteriaCommand : IRequest<IEnumerable<TaughtSubject>>
    {
        public Department Department { get; set; }
        public TaughtSubjectType TaughtSubjectType { get; set; }
    }
}
