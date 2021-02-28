using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    public class SubjectBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        public SubjectBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        [Display(Name = "Subject title")]
        [Required(ErrorMessage = "Subject title is required")]
        [RegularExpression(pattern: "[a-zA-Z\\s]+", ErrorMessage = "Invalid text")]
        public string SubjectName { get; set; }
        
        [BindProperty]
        [Display(Name = "Number of credits")]
        [Required(ErrorMessage = "Number of credits is required")]
        [Range(1, 5, ErrorMessage = "Number of credits must be between 1 and 5")]
        public int? NumberOfCredits { get; set; }
    }
}
