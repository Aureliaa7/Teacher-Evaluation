using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.BusinessLogic.ViewModels;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Grades
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public IEnumerable<GradeVm> Grades { get; set; } = new List<GradeVm>();

        public async Task OnGet(Guid subjectId, Guid teacherId, TaughtSubjectType type, int year)
        {
            var command = new SearchGradesByCriteriaCommand
            {
                SubjectId = subjectId,
                FromYear = year,
                TeacherId = teacherId,
                Type = type
            };
            try
            {
                Grades = await mediator.Send(command);
            }
            catch (ItemNotFoundException)
            {
            }
        }
    }
}
