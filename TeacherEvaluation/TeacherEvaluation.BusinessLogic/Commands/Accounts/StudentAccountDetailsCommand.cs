using MediatR;
using System;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class StudentAccountDetailsCommand : IRequest<StudentAccountDetailsVm>
    {
        public Guid UserId { get; set; }
    }
}
