﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Guid StudentId { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [BindProperty]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [BindProperty]
        [Display(Name = "Father's initial")]
        public string FathersInitial { get; set; }

        [BindProperty]
        [Display(Name = "Study year")]
        public int? StudyYear { get; set; } = null;

        [BindProperty]
        public Specialization Specialization { get; set; }

        [BindProperty]
        public string Group { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            StudentId = (Guid)id;
            GetStudentByIdCommand command = new GetStudentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Student student = await mediator.Send(command);
                FirstName = student.User.FirstName;
                LastName = student.User.LastName;
                Email = student.User.Email;
                FathersInitial = student.User.FathersInitial;
                PIN = student.User.PIN;
                Group = student.Group;
                Specialization = student.Specialization;
                StudyYear = student.StudyYear;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }
    }
}
