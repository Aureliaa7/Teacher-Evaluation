using MediatR;
using TeacherEvaluation.BusinessLogic.Helpers;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
