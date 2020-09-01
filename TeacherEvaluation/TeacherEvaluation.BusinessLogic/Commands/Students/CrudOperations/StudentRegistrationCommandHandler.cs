﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class StudentRegistrationCommandHandler : IRequestHandler<StudentRegistrationCommand, List<string>>
    {
        private readonly IRepository<Student> studentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;
        public StudentRegistrationCommandHandler(IRepository<Student> studentRepository, UserManager<ApplicationUser> userManager, INotificationService emailService)
        {
            this.studentRepository = studentRepository;
            this.userManager = userManager;
            this.emailService = emailService;
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
                UserName = request.Email
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

                Student student = new Student
                {
                    StudyProgramme = request.StudyProgramme,
                    Group = request.Group,
                    Section = request.Section,
                    StudyYear = request.StudyYear,
                    PIN = request.PIN,
                    User = newApplicationUser
                };
                await studentRepository.Add(student);

                Notification notification = ConfigureNotificationMessage(confirmationUrl, newApplicationUser, request.Password);
                emailService.Send(notification);

                errorMessages = null;
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

        private Notification ConfigureNotificationMessage(string url, ApplicationUser newApplicationUser, string password)
        {
            Notification notification = CreateNotificationMessage(url, newApplicationUser, password);

            NotificationAddress recipient = GetRecipientAddress(newApplicationUser);
            notification.ToAddresses.Add(recipient);
            notification.FromAddress = new NotificationAddress("noreply-teacher.evaluation", "teacher.evaluation.project2021@gmail.com");

            return notification;
        }

        private Notification CreateNotificationMessage(string url, ApplicationUser newApplicationUser, string password)
        {
            string bodyResourceName = "TeacherEvaluation.BusinessLogic.NotificationTemplates.EmailConfirmationNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.BusinessLogic.NotificationTemplates.EmailConfirmationNotificationSubject.txt";

            string notificationBody = ResourceProvider.GetResourceText(bodyResourceName);
            string notificationSubject = ResourceProvider.GetResourceText(subjectResourceName);
            string recipientName = GetRecipientAddress(newApplicationUser).Name;
            string loginPageUrl = url;
            string accountPassword = password;

            return new Notification
            {
                Subject = notificationSubject,
                Content = string.Format(notificationBody, recipientName, loginPageUrl, accountPassword)
            };
        }

        private NotificationAddress GetRecipientAddress(ApplicationUser newApplicationUser)
        {
            ApplicationUser user = newApplicationUser;
            string name = string.Join(" ", newApplicationUser.LastName, newApplicationUser.FathersInitial, newApplicationUser.FirstName); 
            return new NotificationAddress(name, user.Email);
        }
    }
}
