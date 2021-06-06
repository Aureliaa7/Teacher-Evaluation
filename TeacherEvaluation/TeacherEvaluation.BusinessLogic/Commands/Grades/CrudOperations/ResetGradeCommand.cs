using MediatR;
using System;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class ResetGradeCommand : IRequest
    {
        public Guid GradeId { get; set; }
    }
}
