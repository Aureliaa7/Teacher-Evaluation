﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands;

namespace TeacherEvaluation.Application.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IMediator mediator;
        public List<string> errorMessages;


        [BindProperty]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(IMediator mediator)
        {
            this.mediator = mediator;
            errorMessages = new List<string>();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                LoginCommand loginUserCommand = new LoginCommand { Email = Email, Password = Password };
                List<string> responseErrors = await mediator.Send(loginUserCommand);

                if (responseErrors == null)
                {
                    return RedirectToPage("../Index");
                }

                errorMessages = responseErrors;
            }
            return Page();
        }

    }
}