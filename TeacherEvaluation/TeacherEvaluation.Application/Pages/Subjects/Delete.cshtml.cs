using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Subjects
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : SubjectBaseModel
    {
        [BindProperty]
        public Guid SubjectId { get; set; }

        public DeleteModel(IMediator mediator): base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if(id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            SubjectId = (Guid)id;
            GetSubjectByIdCommand command = new GetSubjectByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Subject subjectToBeDeleted = await mediator.Send(command);
                SubjectName = subjectToBeDeleted.Name;
                NumberOfCredits = subjectToBeDeleted.NumberOfCredits;
                StudyYear = subjectToBeDeleted.StudyYear;
                Specialization = subjectToBeDeleted.Specialization;
                Semester = subjectToBeDeleted.Semester;
            }
            catch(ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteSubjectCommand command = new DeleteSubjectCommand
                {
                    Id =SubjectId
                };
                await mediator.Send(command);
            }
            catch(ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return RedirectToPage("../Subjects/Index");
        }
    }
}
