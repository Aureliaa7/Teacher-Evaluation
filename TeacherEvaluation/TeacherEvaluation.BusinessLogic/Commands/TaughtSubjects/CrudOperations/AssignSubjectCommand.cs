using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations
{
    public class AssignSubjectCommand : IRequest
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
        public StudyProgramme StudyProgramme { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
    }
}
