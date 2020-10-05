using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudentsByCriteriaCommand : IRequest<IEnumerable<Student>>
    {
        public StudyProgramme StudyProgramme { get; set; }
        public Guid StudyDomainId { get; set; }
        public Guid SpecializationId { get; set; }
        public int StudyYear { get; set; }
    }
}
