using MediatR;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class GetStudyDomainsByProgrammeCommand : IRequest<IEnumerable<StudyDomain>>
    {
        public StudyProgramme StudyProgramme { get; set; }
    }
}
