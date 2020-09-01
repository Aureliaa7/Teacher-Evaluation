using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TeacherEvaluation.DataAccess.Repositories;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class TeacherRegistrationCommandHandler : IRequestHandler<TeacherRegistrationCommand, List<string>>
    {
        private readonly IRepository<Teacher> teacherRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;
        public TeacherRegistrationCommandHandler(IRepository<Teacher> teacherRepository, UserManager<ApplicationUser> userManager, INotificationService emailService)
        {
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        public async Task<List<string>> Handle(TeacherRegistrationCommand request, CancellationToken cancellationToken)
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

                await userManager.AddToRoleAsync(newApplicationUser, "Teacher");

                Teacher teacher = new Teacher
                {
                    Degree = request.Degree,
                    Department = request.Department,
                    PIN = request.PIN,
                    User = newApplicationUser
                };
                await teacherRepository.Add(teacher);

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

        private Notification CreateNotificationMessage(string loginUrl, ApplicationUser newApplicationUser, string password)
        {
            string bodyResourceName = "TeacherEvaluation.BusinessLogic.NotificationTemplates.EmailConfirmationNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.BusinessLogic.NotificationTemplates.EmailConfirmationNotificationSubject.txt";

            string notificationBody = ResourceProvider.GetResourceText(bodyResourceName);
            string notificationSubject = ResourceProvider.GetResourceText(subjectResourceName);
            string recipientName = GetRecipientAddress(newApplicationUser).Name;

            return new Notification
            {
                Subject = notificationSubject,
                Content = string.Format(notificationBody, recipientName, loginUrl, password)
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
