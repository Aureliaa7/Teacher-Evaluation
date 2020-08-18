using MediatR;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
