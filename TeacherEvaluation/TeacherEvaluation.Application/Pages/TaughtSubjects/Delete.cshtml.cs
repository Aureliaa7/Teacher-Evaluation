using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.TaughtSubjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.TaughtSubjects
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : TaughtSubjectBaseModel
    {
        public DeleteModel(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            TaughtSubjectId = (Guid)id;
            GetTaughtSubjectByIdCommand command = new GetTaughtSubjectByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                TaughtSubject taughtSubjectToBeDeleted = await mediator.Send(command);
                TeacherName = taughtSubjectToBeDeleted.Teacher.User.FirstName + " " + taughtSubjectToBeDeleted.Teacher.User.LastName;
                SubjectTitle = taughtSubjectToBeDeleted.Subject.Name;
                Type = taughtSubjectToBeDeleted.Type;
                Year = taughtSubjectToBeDeleted.StudyYear;
                Semester = taughtSubjectToBeDeleted.Semester;
                StudyProgramme = taughtSubjectToBeDeleted.StudyProgramme;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                DeleteTaughtSubjectCommand command = new DeleteTaughtSubjectCommand
                {
                    Id = TaughtSubjectId
                };
                await mediator.Send(command);
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return RedirectToPage("../TaughtSubjects/Index");
        }
    }
}
