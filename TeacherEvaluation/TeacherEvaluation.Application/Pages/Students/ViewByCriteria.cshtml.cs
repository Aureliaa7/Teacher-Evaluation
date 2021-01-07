using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Students
{
    public class ViewByCriteriaModel : PageModel
    {
        private readonly IMediator mediator;
        public bool TableIsVisible;

        [BindProperty]
        public IEnumerable<Student> Students { get; set; }

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "The study programme is required")]
        public StudyProgramme StudyProgramme { get; set; }
        
        [BindProperty]
        [Required(ErrorMessage = "The study domain is required")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The specialization is required")]
        public Guid SpecializationId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        public ViewByCriteriaModel(IMediator mediator)
        {
            this.mediator = mediator;
            Students = new List<Student>();
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                TableIsVisible = true;
                GetStudentsByCriteriaCommand command = new GetStudentsByCriteriaCommand
                {
                    SpecializationId = SpecializationId,
                    StudyDomainId = StudyDomainId,
                    StudyProgramme = StudyProgramme,
                    StudyYear = (int)StudyYear
                };

                Students = await mediator.Send(command);
            }
        }
    }
}
