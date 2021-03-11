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

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class StudentRegistrationCommandHandler : IRequestHandler<StudentRegistrationCommand, List<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;

        public StudentRegistrationCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, 
            INotificationService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        public async Task<List<string>> Handle(StudentRegistrationCommand request, CancellationToken cancellationToken)
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

                await userManager.AddToRoleAsync(newApplicationUser, "Student");

                bool specializationExists = await unitOfWork.SpecializationRepository.Exists(x => x.Id == request.SpecializationId);
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
                    await unitOfWork.StudentRepository.Add(student);
                    await unitOfWork.SaveChangesAsync();

                    Notification notification = EmailSending.ConfigureAccountCreationMessage(confirmationUrl, newApplicationUser, randomPassword);
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
    }
}
