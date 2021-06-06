using MediatR;
using System;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class SearchGradesByCriteriaCommand : IRequest<IEnumerable<GradeVm>>
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public TaughtSubjectType Type { get; set; }
        public int FromYear { get; set; }
    }
}
