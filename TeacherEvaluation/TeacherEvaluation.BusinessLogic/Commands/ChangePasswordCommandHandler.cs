﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, List<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public List<string> errorMessages;

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            errorMessages = new List<string>();
        }

        public async Task<List<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.CurrentUserName);
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var errorMessage in result.Errors)
                {
                    errorMessages.Add(errorMessage.Description);
                }
            }
            else
            {
                errorMessages = null;
            }
            return errorMessages;
        }
    }
}
