using MediatR;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class ConfirmEmailCommand : IRequest<List<string>>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
