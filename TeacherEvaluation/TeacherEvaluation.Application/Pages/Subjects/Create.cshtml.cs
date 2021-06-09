﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : SubjectBaseModel
    {
        public CreateModel(IMediator mediator): base(mediator)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelIsValid())
            {
                AddSubjectCommand command = new AddSubjectCommand
                {
                    Name = SubjectName,
                    NumberOfCredits = (int)NumberOfCredits,
                    StudyYear = (int) StudyYear,
                    SpecializationId = SpecializationId,
                    Semester = Semester
                };
                await mediator.Send(command);
                return RedirectToPage("/Subjects/Index");
            }
            return Page();
        }

        private bool ModelIsValid()
        {
            return (SubjectName != null && NumberOfCredits != null && StudyYear != null && SpecializationId != null);
        }
    }
}
