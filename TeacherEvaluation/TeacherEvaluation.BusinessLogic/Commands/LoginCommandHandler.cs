using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, List<string>>
    {
        public List<string> errorMessages;

        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            errorMessages = new List<string>();
        }

        public async Task<List<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await signInManager.PasswordSignInAsync(command.Email,command.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                errorMessages = null;
            }
            else
            {
                errorMessages.Add("Invalid login attempt");
            }
            return errorMessages;
        }
    }
}
