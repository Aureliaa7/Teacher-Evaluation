using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherEvaluation.Application.Pages.Shared
{
    public class InsertFromFileBaseModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        [Required]
        public IFormFile ExcelFile { get; set; }

        [BindProperty]
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public InsertFromFileBaseModel(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
