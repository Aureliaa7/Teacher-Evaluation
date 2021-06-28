using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;
using TeacherEvaluation.EmailSender.NotificationService;
using TeacherEvaluation.EmailSender.NotificationService.Interfaces;

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

                await userManager.AddToRoleAsync(newApplicationUser, "Teacher");

                Teacher teacher = new Teacher
                {
                    Degree = request.Degree,
                    Department = request.Department,
                    User = newApplicationUser
                };
                await unitOfWork.TeacherRepository.AddAsync(teacher);
                await unitOfWork.SaveChangesAsync();

                Notification notification = EmailSending.ConfigureAccountCreationMessage(confirmationUrl, newApplicationUser, request.Password);
                emailService.Send(notification);

                errorMessages = null;
            }
            else
            {
                var user = await unitOfWork.UserRepository.GetAsync(x => x.Email.Equals(request.Email));
                if (user != null)
                {
                    await unitOfWork.UserRepository.RemoveAsync(user.Id);
                    await unitOfWork.SaveChangesAsync();
                }
                foreach (var errorMessage in result.Errors)
                {
                    errorMessages.Add(errorMessage.Description);
                }
            }
            return errorMessages;
        }
    }
}
