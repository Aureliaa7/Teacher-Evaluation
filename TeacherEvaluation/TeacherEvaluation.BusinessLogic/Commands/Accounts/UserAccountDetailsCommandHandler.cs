using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.Identity;

namespace TeacherEvaluation.BusinessLogic.Commands.Accounts
{
    public class UserAccountDetailsCommandHandler : IRequestHandler<UserAccountDetailsCommand, UserAccountDetailsVm>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public UserAccountDetailsCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<UserAccountDetailsVm> Handle(UserAccountDetailsCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await unitOfWork.UserRepository.ExistsAsync(u => u.Id == request.UserId);
            if(userExists)
            {
                var user = await unitOfWork.UserRepository.GetAsync(request.UserId);
                string role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
                return new UserAccountDetailsVm
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FathersInitial = user.FathersInitial,
                    PIN = user.PIN,
                    Role = role,
                    Email = user.Email
                };
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
