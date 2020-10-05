using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations
{
    public class GetEnrollmentsByCriteriaCommand : IRequest<IEnumerable<Enrollment>>
    {
        public StudyProgramme StudyProgramme { get; set; }
        public Guid StudyDomainId { get; set; }
        public Guid SpecializationId { get; set; }
        public TaughtSubjectType TaughtSubjectType { get; set; }
        public EnrollmentState EnrollmentState { get; set; }
        public int StudyYear { get; set; }
    }
}
