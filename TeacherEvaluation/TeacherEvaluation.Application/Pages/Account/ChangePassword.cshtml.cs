﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Accounts;

namespace TeacherEvaluation.Application.Pages.Account
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "New password is required")]
        [RegularExpression(pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public ChangePasswordModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
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

                ErrorMessages = await mediator.Send(changePasswordCommand);

                if (!ErrorMessages.Any())
                {
                    string role = User.FindFirstValue(ClaimTypes.Role);

                    return RedirectToPage($"/MyProfile/{role}");
                }
            }
            return Page();
        }
    }
}