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
    public class EditModel : SubjectBaseModel
    {
        public EditModel(IMediator mediator): base(mediator)
        { 
        }

        [BindProperty]
        public Guid SubjectId { get; set; }

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
                Subject subject = await mediator.Send(command);
                SubjectName = subject.Name;
                NumberOfCredits = subject.NumberOfCredits;
                StudyYear = subject.StudyYear;
                Specialization = subject.Specialization;
            }
            catch(ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateSubjectCommand command = new UpdateSubjectCommand
                    {
                        Id = SubjectId,
                        Name = SubjectName,
                        NumberOfCredits = (int)NumberOfCredits,
                        StudyYear = (int)StudyYear,
                        SpecializationId = SpecializationId,
                        Semester = Semester
                    };
                    await mediator.Send(command);
                    return RedirectToPage("/Subjects/Index");
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("/Errors/404");
                }
            }
            return Page();
        }
    }
}
