using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TeacherEvaluation.BusinessLogic.PasswordGenerator;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;
using TeacherEvaluation.EmailSender.NotificationService;

namespace TeacherEvaluation.BusinessLogic.Commands.Teachers.CrudOperations
{
    public class TeacherRegistrationCommandHandler : IRequestHandler<TeacherRegistrationCommand, List<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;
        public TeacherRegistrationCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, INotificationService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        public async Task<List<string>> Handle(TeacherRegistrationCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = new List<string>();
            string randomPassword = RandomPasswordGenerator.GeneratePassword(15);

            ApplicationUser newApplicationUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FathersInitial = request.FathersInitial,
                Email = request.Email,
                UserName = request.Email,
                PIN = request.PIN
            };

            var result = await userManager.CreateAsync(newApplicationUser, randomPassword);

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
                    User = newApplicationUser
                };
                await unitOfWork.TeacherRepository.Add(teacher);
                await unitOfWork.SaveChangesAsync();

                Notification notification = EmailSending.ConfigureAccountCreationMessage(confirmationUrl, newApplicationUser, randomPassword);
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
    }
}
