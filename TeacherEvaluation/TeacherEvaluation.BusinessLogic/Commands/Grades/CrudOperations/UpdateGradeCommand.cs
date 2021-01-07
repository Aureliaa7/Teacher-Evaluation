using MediatR;
using System;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class UpdateGradeCommand : IRequest
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
