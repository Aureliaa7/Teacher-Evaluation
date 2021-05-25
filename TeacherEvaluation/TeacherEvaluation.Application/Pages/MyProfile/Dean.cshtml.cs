using MediatR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.Application.Pages.MyProfile;
using TeacherEvaluation.BusinessLogic.Commands.Accounts;
using TeacherEvaluation.BusinessLogic.Exceptions;

namespace TeacherEvaluation.Application.Pages.Dashboards
{
    public class DeanModel : UserDetailsModel
    {
        public DeanModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGet()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var getUserDetailsCommand = new UserAccountDetailsCommand { UserId = currentUserId };
                UserDetailsVm = await mediator.Send(getUserDetailsCommand);
            }
            catch (ItemNotFoundException)
            {
            }
        }
    }
}