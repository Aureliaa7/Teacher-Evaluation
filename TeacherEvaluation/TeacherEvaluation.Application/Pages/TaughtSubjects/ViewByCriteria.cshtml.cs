using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles= "Administrator")]
    public class ViewByCriteriaModel : PageModel
    {
        private readonly IMediator mediator;
        public bool TableIsVisible;

        [BindProperty]
        public IEnumerable<TaughtSubject> TaughtSubjects { get; set; }

        [BindProperty]
        [EnumDataType(typeof(Department))]
        [Required(ErrorMessage = "The department is required")]
        public Department Department { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        [Required(ErrorMessage = "The subject type is required")]
        public TaughtSubjectType TaughtSubjectType { get; set; }

        public ViewByCriteriaModel(IMediator mediator)
        {
            this.mediator = mediator;
            TaughtSubjects = new List<TaughtSubject>();
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                TableIsVisible = true;
                GetTaughtSubjectsByCriteriaCommand command = new GetTaughtSubjectsByCriteriaCommand
                {
                    Department = Department, 
                    TaughtSubjectType = TaughtSubjectType
                };
                TaughtSubjects = await mediator.Send(command);
            }
        }
    }
}
