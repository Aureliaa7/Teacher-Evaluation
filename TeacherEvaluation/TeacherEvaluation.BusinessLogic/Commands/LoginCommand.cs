using MediatR;
using System.Collections.Generic;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class LoginCommand : IRequest<List<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
