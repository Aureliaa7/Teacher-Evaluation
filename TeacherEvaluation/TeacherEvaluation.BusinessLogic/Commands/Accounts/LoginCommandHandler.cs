using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly LoginResult loginResult;
        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            loginResult = new LoginResult();
        }

        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await signInManager.PasswordSignInAsync(command.Email,command.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(command.Email);
                var roles = await userManager.GetRolesAsync(user);
                loginResult.UserRole = roles.FirstOrDefault();
            }
            else
            {
                loginResult.ErrorMessages.Add("Wrong credentials");
            }
            return loginResult;
        }
    }
}
