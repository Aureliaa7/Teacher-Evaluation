using MediatR;
using System;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class UserAccountDetailsCommand : IRequest<UserAccountDetailsVm>
    {
        public Guid UserId { get; set; } 
    }
}
