﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Grades.CrudOperations;
using TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Grades
{
    [Authorize(Roles ="Administrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        [BindProperty]
        [Required(ErrorMessage = "Student is required")]
        public Guid StudentId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Subject is required")]
        public Guid SubjectId { get; set; }

        [BindProperty]
        [EnumDataType(typeof(TaughtSubjectType))]
        public TaughtSubjectType Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Grade is required")]
        public int? Grade { get; set; } = null;

        [BindProperty]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateTime { get; set; }

        [BindProperty]
        [EnumDataType(typeof(StudyProgramme))]
        [Required(ErrorMessage = "Study programme is required")]
        public StudyProgramme StudyProgramme { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Study domain is required")]
        public Guid StudyDomainId { get; set; }

        [BindProperty]
        [Display(Name = "Study domain")]
        [Required(ErrorMessage = "Specialization is required")]
        public Guid SpecializationId { get; set; }

        [BindProperty]
        [Display(Name = "Study year")]
        [Required(ErrorMessage = "Study year is required")]
        [Range(1, 4, ErrorMessage = "The study year must be between 1 and 4")]
        public int? StudyYear { get; set; } = null;

        public List<SelectListItem> TaughtSubjects { get; set; }

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public IActionResult OnGetReturnSubjects(string studentId)
        {
            GetUserIdForStudentCommand getUserIdCommand = new GetUserIdForStudentCommand { StudentId = new Guid(studentId) };
            try
            {
                Guid userIdStudent = mediator.Send(getUserIdCommand).Result;
                GetSubjectsForEnrollmentsCommand getSubjectsCommand = new GetSubjectsForEnrollmentsCommand
                {
                    UserId = userIdStudent,
                    EnrollmentState = EnrollmentState.InProgress
                };
                var subjects = mediator.Send(getSubjectsCommand).Result;
                return new JsonResult(subjects);
            }
            catch (ItemNotFoundException e)
            {
                return new JsonResult(e.Message);
            }
        }

        public IActionResult OnGetCheckEnrollment(string studentId, string subjectId, string type)
        {
            EnrollmentExistsCommand command = new EnrollmentExistsCommand
            {
                StudentId = new Guid(studentId),
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            bool enrollmentExists = mediator.Send(command).Result;
            if (enrollmentExists)
            {
                return new JsonResult("The enrollment exists");
            }
            return new JsonResult("The enrollment does not exist");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                UpdateGradeCommand command = new UpdateGradeCommand
                {
                    SubjectId = SubjectId,
                    StudentId = StudentId,
                    Type = Type,
                    Value = (int)Grade,
                    Date = (DateTime)DateTime
                };
                try
                {
                    await mediator.Send(command);
                }
                catch (ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }
                return RedirectToPage("../Students/Index");
            }
            return Page();
        }
    }
}
