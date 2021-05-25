using MediatR;
using System;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class TeacherAccountDetailsCommand : IRequest<TeacherAccountDetailsVm>
    {
        public Guid UserId { get; set; }
    }
}
