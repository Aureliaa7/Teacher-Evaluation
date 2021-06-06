using MediatR;
using System;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations
{
    public class GetGradeByIdCommand : IRequest<GradeVm>
    {
        public Guid Id { get; set; }
    }
}
