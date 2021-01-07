using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.DomainEntities;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender;
using TeacherEvaluation.EmailSender.NotificationModel;
using TeacherEvaluation.Domain.DomainEntities.Enums;


namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms.QuestionsWithOptionAnswer
{
    public class CreateFormForQuestionWithOptionCommandHandler : AsyncRequestHandler<CreateFormForQuestionWithOptionCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;

        public CreateFormForQuestionWithOptionCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, INotificationService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        protected override async Task Handle(CreateFormForQuestionWithOptionCommand request, CancellationToken cancellationToken)
        {
            Form form = new Form
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EnrollmentState = request.EnrollmentState,
                Type = FormType.Option, 
                MinNumberOfAttendances = request.MinNumberAttendances
            };
            await unitOfWork.FormRepository.Add(form);

            string startDate = request.StartDate.ToString("yyyy-MM-dd hh:mm:ss");
            string endDate = request.EndDate.ToString("yyyy-MM-dd hh:mm:ss");
            string interval = string.Join(" - ", startDate, endDate);
            Notification notification = ConfigureNotificationMessage(interval);
            emailService.Send(notification);

            foreach (var question in request.Questions)
            {
                QuestionWithOptionAnswer newQuestion = new QuestionWithOptionAnswer
                {
                    Question = question,
                    Form = form
                };
                await unitOfWork.QuestionWithOptionAnswerRepository.Add(newQuestion);
            }
        }

        private Notification ConfigureNotificationMessage(string interval)
        {
            Notification notification = CreateEvaluateTeacherNotification(interval);

            List<NotificationAddress> recipients = new List<NotificationAddress>();
            GetStudentsAddresses(recipients);
            AddMessageRecipients(recipients, notification);
            AddMessageSender(notification);
            return notification;
        }

        private Notification CreateEvaluateTeacherNotification(string interval)
        {
            string bodyResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EvaluateTeacherNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EvaluateTeacherNotificationSubject.txt";

            string notificationBody = ResourceProvider.GetResourceText(bodyResourceName);
            string notificationSubject = ResourceProvider.GetResourceText(subjectResourceName);

            return new Notification
            {
                Subject = notificationSubject,
                Content = string.Format(notificationBody, interval)
            };
        }

        private void GetStudentsAddresses(List<NotificationAddress> recipients)
        {
            List<ApplicationUser> users = userManager.GetUsersInRoleAsync("Student").Result.ToList();

            foreach (ApplicationUser user in users)
            {
                string name = string.Join(" ", user.LastName, user.FathersInitial, user.FirstName);
                NotificationAddress recipient = new NotificationAddress(name, user.Email);
                recipients.Add(recipient);
            }
        }

        private void AddMessageRecipients(List<NotificationAddress> recipients, Notification notification)
        {
            foreach (NotificationAddress recipient in recipients)
            {
                notification.ToAddresses.Add(recipient);
            }
        }

        private void AddMessageSender(Notification notification)
        {
            notification.FromAddress = new NotificationAddress("noreply-teacher.evaluation", "teacher.evaluation.project2021@gmail.com");
        }
    }
}
