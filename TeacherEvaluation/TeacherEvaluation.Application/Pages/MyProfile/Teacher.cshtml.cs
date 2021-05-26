using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Accounts;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.MyProfile
{
    [Authorize(Roles = "Teacher")]
    public class TeacherModel : UserDetailsModel
    {
        public TeacherModel(IMediator mediator) : base(mediator)
        {
        }

        [BindProperty]
        public string Department { get; set; }

        [BindProperty]
        public string Degree { get; set; }

        public async Task OnGet()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var teacherAccountDetailsCommand = new TeacherAccountDetailsCommand { UserId = currentUserId };
                var teacherAccountDetails = await mediator.Send(teacherAccountDetailsCommand);
                InitializeFields(teacherAccountDetails);
            }
            catch (ItemNotFoundException) { }
        }

        private void InitializeFields(TeacherAccountDetailsVm teacherAccountVm)
        {
            UserDetailsVm = new UserAccountDetailsVm
            {
                FirstName = teacherAccountVm.FirstName,
                LastName = teacherAccountVm.LastName,
                FathersInitial = teacherAccountVm.FathersInitial,
                Email = teacherAccountVm.Email,
                PIN = teacherAccountVm.PIN,
                Role = teacherAccountVm.Role
            };
            Department = teacherAccountVm.Department;
            Degree = teacherAccountVm.Degree;
        }
    }
}