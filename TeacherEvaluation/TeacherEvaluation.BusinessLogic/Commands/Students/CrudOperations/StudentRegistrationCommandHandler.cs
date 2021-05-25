using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TeacherEvaluation.BusinessLogic.Commands.Enrollments.CrudOperations;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.DomainEntities.Enums;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;
using TeacherEvaluation.EmailSender.NotificationService;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class StudentRegistrationCommandHandler : IRequestHandler<StudentRegistrationCommand, List<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;
        private readonly IMediator mediator;

        public StudentRegistrationCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, 
            INotificationService emailService, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.emailService = emailService;
            this.mediator = mediator;
        }

        public async Task<List<string>> Handle(StudentRegistrationCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();

            ApplicationUser newApplicationUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FathersInitial = request.FathersInitial,
                Email = request.Email,
                UserName = request.Email,
                PIN = request.PIN
            };

            var result = await userManager.CreateAsync(newApplicationUser, request.Password);

            if (result.Succeeded)
            {
                string token = await userManager.GenerateEmailConfirmationTokenAsync(newApplicationUser);
                string tokenHtmlVersion = HttpUtility.UrlEncode(token);
                string confirmationUrl = request.ConfirmationUrlTemplate
                    .Replace("((token))", tokenHtmlVersion)
                    .Replace("((userId))", newApplicationUser.Id.ToString());

                await userManager.AddToRoleAsync(newApplicationUser, "Student");

                bool specializationExists = await unitOfWork.SpecializationRepository.ExistsAsync(x => x.Id == request.SpecializationId);
                if (specializationExists)
                {
                    var specialization = await unitOfWork.SpecializationRepository.GetSpecialization(request.SpecializationId);

                    Student student = new Student
                    {
                        Specialization = specialization,
                        Group = request.Group,
                        StudyYear = request.StudyYear,
                        User = newApplicationUser
                    };
                    await unitOfWork.StudentRepository.AddAsync(student);
                    await unitOfWork.SaveChangesAsync();
                    await EnrollStudentToCourses(student.Id, request.SpecializationId, request.StudyYear);
                    await unitOfWork.SaveChangesAsync();

                    Notification notification = EmailSending.ConfigureAccountCreationMessage(confirmationUrl, newApplicationUser, request.Password);
                    emailService.Send(notification);

                    errorMessages = null;
                }
                else
                {
                    errorMessages.Add("The specialization does not exist");
                }
            }
            else
            {
                foreach (var errorMessage in result.Errors)
                {
                    errorMessages.Add(errorMessage.Description);
                }
            }
            return errorMessages;
        }

        private async Task EnrollStudentToCourses(Guid studentId, Guid specializationId, int studyYear)
        {
            var subjects = await unitOfWork.SubjectRepository.GetSubjectsByCriteria(specializationId, studyYear);
            foreach(var subject in subjects)
            {
                var taughtSubjects = await unitOfWork.TaughtSubjectRepository.GetTaughtSubjectsByCriteria(ts => ts.Subject.Id == subject.Id &&
                                                                                                          ts.Type == TaughtSubjectType.Course);
                foreach (var taughtSubject in taughtSubjects)
                {
                    EnrollStudentCommand command = new EnrollStudentCommand
                    {
                        TeacherId = taughtSubject.Teacher.Id,
                        SubjectId = subject.Id,
                        StudentId = studentId,
                        Type = TaughtSubjectType.Course
                    };
                    try
                    {
                        await mediator.Send(command);
                    }
                    catch (ItemNotFoundException)
                    {
                        throw;
                    }
                }
            }   
        }
    }
}
