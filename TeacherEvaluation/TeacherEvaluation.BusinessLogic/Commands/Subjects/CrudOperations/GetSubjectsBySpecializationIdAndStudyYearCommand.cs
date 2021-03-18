using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations
{
    public class GetSubjectsBySpecializationIdAndStudyYearCommand : IRequest<IEnumerable<Subject>>
    {
        public Guid SpecializationId { get; set; }
        public int StudyYear { get; set; }
    }
}
