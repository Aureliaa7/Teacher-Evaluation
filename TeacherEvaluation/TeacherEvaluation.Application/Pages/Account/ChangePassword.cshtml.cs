using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands;

namespace TeacherEvaluation.Application.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IMediator mediator;

        public bool IsAdministrator { get; private set; } 
        public bool IsDean { get; private set; }
        public bool IsTeacher { get; private set; }
        public bool IsStudent { get; private set; }

        [BindProperty]
        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "New password is required")]
        [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmedPassword { get; set; }

        public List<string> ErrorMessages { get; set; }

        public ChangePasswordModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
            IsAdministrator = User.IsInRole("Administrator");
            IsDean = User.IsInRole("Dean");
            IsStudent = User.IsInRole("Student");
            IsTeacher = User.IsInRole("Teacher");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ChangePasswordCommand changePasswordCommand = new ChangePasswordCommand
                {
                    CurrentUserName = User.Identity.Name,
                    CurrentPassword = CurrentPassword,
                    NewPassword = NewPassword
                };

                List<string> responseError = await mediator.Send(changePasswordCommand);

                if (responseError == null)
                {
                    return RedirectToPage("../Index");
                }
                ErrorMessages = responseError;
            }
            return Page();
        }
    }
}