using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.MyProfile
{
    public class UserDetailsModel : PageModel
    {
        protected readonly IMediator mediator;

        public UserDetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public UserAccountDetailsVm UserDetailsVm { get; set; }
    }
}