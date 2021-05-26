using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

namespace TeacherEvaluation.Application.Pages.Students
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : StudentBaseModel
    {
        [BindProperty]
        public List<SelectListItem> StudyProgrammes { get; set; } = new List<SelectListItem>();

        public EditModel(IMediator mediator): base(mediator)
        {
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            StudentId = (Guid)id;
            GetStudentByIdCommand command = new GetStudentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Student student = await mediator.Send(command);
                FirstName = student.User.FirstName;
                LastName = student.User.LastName;
                Email = student.User.Email;
                FathersInitial = student.User.FathersInitial;
                PIN = student.User.PIN;
                Group = student.Group;
                Specialization = student.Specialization;
                StudyDomainId = StudyDomainId;
                SpecializationId = student.Specialization.Id;
                StudyYear = student.StudyYear;

                InitializeStudyProgrammes(Specialization.StudyDomain.StudyProgramme.ToString());
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        private void InitializeStudyProgrammes(string currentStudyProgramme)
        {
            foreach (var studyProgramme in Enum.GetValues(typeof(StudyProgramme)))
            {
                if (studyProgramme.ToString() != currentStudyProgramme)
                {
                    StudyProgrammes.Add(new SelectListItem
                    {
                        Text = studyProgramme.ToString(),
                        Value = ((int)studyProgramme).ToString()
                    });
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelIsValid())
            {
                try
                {
                    UpdateStudentCommand command = new UpdateStudentCommand
                    {
                        Id = (Guid)StudentId,
                        Email = Email,
                        FathersInitial = FathersInitial,
                        FirstName = FirstName,
                        Group = Group,
                        LastName = LastName,
                        PIN = PIN,
                        SpecializationId = (Guid)SpecializationId,
                        StudyYear = (int)StudyYear
                    };
                    await mediator.Send(command);
                    return RedirectToPage("../Students/Index");
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }
            }
            return Page();
        }

        private bool ModelIsValid()
        {
            return (StudentId != null && Email != null && FathersInitial != null && FirstName != null &&
                LastName != null && Group != null && PIN != null && SpecializationId != null && StudyYear != null);
        }
    }
}

