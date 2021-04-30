using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TeacherEvaluation.Application.Pages.AllResponses
{
    public class ResponseSearchDataModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        public Guid TeacherId { get; set; }

        [BindProperty]
        public Guid FormId { get; set; }

        [BindProperty]
        public string SelectedSubjectId { get; set; }

        public ResponseSearchDataModel(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
