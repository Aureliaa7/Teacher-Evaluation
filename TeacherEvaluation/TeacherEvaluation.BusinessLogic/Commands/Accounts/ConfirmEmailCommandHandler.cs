using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, List<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<List<string>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
        {

            List<string> errorMessages = new List<string>();

            ApplicationUser currentUser = await userManager.FindByIdAsync(command.UserId);

            if (currentUser == null)
            {
                errorMessages.Add($"Unable to load user with ID '{currentUser.Id}'.");
            }

            IdentityResult result = await userManager.ConfirmEmailAsync(currentUser, command.Token);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.Description);
                }
            }

            return errorMessages;
        }
    }
}
