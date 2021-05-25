using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.Application.Pages.MyProfile;
using TeacherEvaluation.BusinessLogic.Commands.Accounts;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.Dashboards
{
    public class StudentModel : UserDetailsModel
    {
        [BindProperty]
        [Display(Name = "Study Year")]
        public int StudyYear { get; set; }

        [BindProperty]
        public string Group { get; set; }

        [BindProperty]
        public string Specialization { get; set; }

        [BindProperty]
        [Display(Name = "Study Domain")]
        public string StudyDomain { get; set; }

        [BindProperty]
        [Display(Name = "Study Programme")]
        public string StudyProgramme { get; set; }

        public StudentModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task OnGet()
        {
            Guid currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var studentDetailsCommand = new StudentAccountDetailsCommand { UserId = currentUserId };
                var studentAccountDetailsVm = await mediator.Send(studentDetailsCommand);
                InitializeProperties(studentAccountDetailsVm);
            }
            catch(ItemNotFoundException) { }
        }

        private void InitializeProperties(StudentAccountDetailsVm studentAccountVm)
        {
            UserDetailsVm = new UserAccountDetailsVm 
            {
                FirstName = studentAccountVm.FirstName,
                LastName = studentAccountVm.LastName,
                FathersInitial = studentAccountVm.FathersInitial,
                Email = studentAccountVm.Email,
                PIN = studentAccountVm.PIN,
                Role = studentAccountVm.Role
            };
            StudyYear = studentAccountVm.StudyYear;
            Specialization = studentAccountVm.Specialization;
            StudyDomain = studentAccountVm.StudyDomain;
            StudyProgramme = studentAccountVm.StudyProgramme;
            Group = studentAccountVm.Group;
        }
    }
}