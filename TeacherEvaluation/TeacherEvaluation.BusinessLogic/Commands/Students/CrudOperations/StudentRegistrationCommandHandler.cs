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
using TeacherEvaluation.EmailSender.NotificationService;

namespace TeacherEvaluation.BusinessLogic.Commands.Students.CrudOperations
{
    public class StudentRegistrationCommandHandler : IRequestHandler<StudentRegistrationCommand, List<string>>
    {
        private readonly IRepository<Student> studentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;
        private readonly ISpecializationRepository specializationRepository;
        public StudentRegistrationCommandHandler(IRepository<Student> studentRepository, UserManager<ApplicationUser> userManager, 
            INotificationService emailService, ISpecializationRepository specializationRepository)
        {
            this.studentRepository = studentRepository;
            this.userManager = userManager;
            this.emailService = emailService;
            this.specializationRepository = specializationRepository;
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

                bool specializationExists = await specializationRepository.Exists(x => x.Id == request.SpecializationId);
                if (specializationExists)
                {
                    var specialization = await specializationRepository.GetSpecialization(request.SpecializationId);

                    Student student = new Student
                    {
                        Specialization = specialization,
                        Group = request.Group,
                        StudyYear = request.StudyYear,
                        PIN = request.PIN,
                        User = newApplicationUser
                    };
                    await studentRepository.Add(student);

                    Notification notification = EmailSending.ConfigureNotificationMessage(confirmationUrl, newApplicationUser, request.Password);
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
