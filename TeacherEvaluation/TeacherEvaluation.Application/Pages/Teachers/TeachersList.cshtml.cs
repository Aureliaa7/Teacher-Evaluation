using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Teachers
{
    public class TeachersListModel : PageModel
    {
        protected readonly IMediator mediator;

        [BindProperty]
        public IEnumerable<Teacher> Teachers { get; set; }

        public TeachersListModel(IMediator mediator)
        {
            this.mediator = mediator;
            Teachers = new List<Teacher>();
        }
    }
}
