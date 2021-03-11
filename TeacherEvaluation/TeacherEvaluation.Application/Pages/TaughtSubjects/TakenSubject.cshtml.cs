using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TeacherEvaluation.BusinessLogic.ViewModels;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    public class TakenSubjectModel : PageModel
    {
        protected IMediator mediator;

        public IEnumerable<TakenSubjectVm> TakenSubjects { get; set; }

        public TakenSubjectModel(IMediator mediator)
        {
            this.mediator = mediator;
            TakenSubjects = new List<TakenSubjectVm>();
        }
    }
}
