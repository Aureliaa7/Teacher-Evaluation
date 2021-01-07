﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherEvaluation.BusinessLogic.Commands.Attendances.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;


namespace TeacherEvaluation.Application.Pages.Attendances
{
    [Authorize(Roles = "Teacher")]
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        public List<SelectListItem> Subjects { get; set; }

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
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateTime { get; set; }

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            Guid currentTeacherId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetSubjectsForTeacherCommand command = new GetSubjectsForTeacherCommand { UserId = currentTeacherId };
            var taughtSubjects = await mediator.Send(command);
            var subjects = new List<Subject>();
            if (taughtSubjects.Count() > 0)
            {
                subjects.Add(taughtSubjects.OrderBy(x => x.Subject.Name).First().Subject);


                Subjects = subjects.Select(x =>
                                                new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.Name
                                                }).ToList();
            }
            return Page();
        }

        public IActionResult OnGetReturnTaughtSubjectTypes(string subjectId)
        {
            Guid currentTeacherId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetSubjectsForTeacherCommand command = new GetSubjectsForTeacherCommand { UserId = currentTeacherId };
            var allTaughtSubjects = mediator.Send(command).Result;
            var taughtSubjects = allTaughtSubjects.Where(x => x.Subject.Id == new Guid(subjectId));
            return new JsonResult(taughtSubjects);
        }

        public IActionResult OnGetReturnEnrolledStudents(string subjectId, string type)
        {
            Guid currentTeacherId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            GetEnrolledStudentsCommand command = new GetEnrolledStudentsCommand
            {
                UserId = currentTeacherId,
                SubjectId = new Guid(subjectId),
                Type = (TaughtSubjectType)Enum.Parse(typeof(TaughtSubjectType), type)
            };
            var students = mediator.Send(command).Result;
            return new JsonResult(students);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                AddAttendanceCommand command = new AddAttendanceCommand
                {
                    Type = Type,
                    DateTime = DateTime,
                    StudentId = StudentId,
                    SubjectId = SubjectId,
                    UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))
                };
                try
                {
                    await mediator.Send(command);
                }
                catch(ItemNotFoundException)
                {
                    return RedirectToPage("../Errors/404");
                }
                return RedirectToPage("../Dashboards/Teacher");
            }
            return Page();
        }
    }
}
