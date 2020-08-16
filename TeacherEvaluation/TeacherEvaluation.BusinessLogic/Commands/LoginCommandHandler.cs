using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Helpers;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands
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
            loginResult.ErrorMessages = new List<string>();
        }

        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await signInManager.PasswordSignInAsync(command.Email,command.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                loginResult.ErrorMessages = null;
                var user = await userManager.FindByNameAsync(command.Email);
                var roles = await userManager.GetRolesAsync(user);
                loginResult.UserRole = roles.ElementAt(0);
            }
            else
            {
                loginResult.ErrorMessages.Add("Invalid login attempt");
            }
            return loginResult;
        }
    }
}
