using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Subjects.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;

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

        [BindProperty]
        public List<SelectListItem> StudyProgrammes { get; set; } = new List<SelectListItem>();
        
        [BindProperty]
        public List<SelectListItem> Semesters { get; set; } = new List<SelectListItem>();

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
                Semester = subject.Semester;
                SpecializationId = subject.Specialization.Id;
                StudyDomainId = subject.Specialization.StudyDomain.Id;
                StudyProgramme = subject.Specialization.StudyDomain.StudyProgramme;

                // these methods are used so that the dropdowns won't have duplicate values
                InitializeStudyProgrammes(StudyProgramme.ToString());
                InitializeSemesters(Semester.ToString());
            }
            catch(ItemNotFoundException)
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

        private void InitializeSemesters(string currentSemester)
        {
            foreach (var semester in Enum.GetValues(typeof(Semester)))
            {
                if (semester.ToString() != currentSemester)
                {
                    Semesters.Add(new SelectListItem
                    {
                        Text = semester.ToString(),
                        Value = ((int)semester).ToString()
                    });
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (IsValidData())
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
        
        private bool IsValidData()
        {
            return SubjectName != null && StudyYear != null && NumberOfCredits > 0;
        }
    }
}
