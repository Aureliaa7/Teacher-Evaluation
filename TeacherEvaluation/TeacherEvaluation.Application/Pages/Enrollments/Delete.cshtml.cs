﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.Domain.DomainEntities;

namespace TeacherEvaluation.Application.Pages.Enrollments
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : EnrollmentBaseModel
    {
        public DeleteModel(IMediator mediator): base(mediator)
        { 
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return RedirectToPage("../Errors/404");
            }
            EnrollmentId = (Guid)id;
            GetEnrollmentByIdCommand command = new GetEnrollmentByIdCommand
            {
                Id = (Guid)id
            };
            try
            {
                Enrollment enrollment = await mediator.Send(command);
                string studentFirstName = enrollment.Student.User.FirstName;
                string studentFathersInitial = enrollment.Student.User.FathersInitial;
                string studentLastName = enrollment.Student.User.LastName;

                string teacherFirstName = enrollment.TaughtSubject.Teacher.User.FirstName;
                string teacherFathersInitial = enrollment.TaughtSubject .Teacher.User.FathersInitial;
                string teacherLastName = enrollment.TaughtSubject.Teacher.User.LastName;

                TeacherName = string.Join(" ", teacherLastName, teacherFathersInitial, teacherFirstName);
                SubjectTitle = enrollment.TaughtSubject.Subject.Name;
                Type = enrollment.TaughtSubject.Type;
                StudentName = string.Join(" ", studentLastName, studentFathersInitial, studentFirstName);
                Group = enrollment.Student.Group;
                Specialization = enrollment.Student.Specialization.Name;
                StudyDomain = enrollment.Student.Specialization.StudyDomain.Name;
                StudyProgramme = enrollment.Student.Specialization.StudyDomain.StudyProgramme;
                StudyYear = enrollment.Student.StudyYear;
            }
            catch (ItemNotFoundException)
            {
                return RedirectToPage("../Errors/404");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DeleteEnrollmentCommand command = new DeleteEnrollmentCommand { Id = (Guid)EnrollmentId };
            await mediator.Send(command);

            return RedirectToPage("./Index");
        }
    }
}
