using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherEvaluation.BusinessLogic.Exceptions;
using TeacherEvaluation.DataAccess.UnitOfWork;
using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.BusinessLogic.Commands.EvaluationForms
{
    public class EditFormCommandHandler : AsyncRequestHandler<EditFormCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationService emailService;

        public EditFormCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, INotificationService notificationService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            emailService = notificationService;
        }

        protected async override Task Handle(EditFormCommand request, CancellationToken cancellationToken)
        {
            bool formExists = await unitOfWork.FormRepository.Exists(x => x.Id == request.FormId);
            if(formExists)
            {
                var form = await unitOfWork.FormRepository.Get(request.FormId);
                form.EndDate = request.EndDate;
                form.StartDate = request.StartDate;
                form.EnrollmentState = request.EnrollmentState;
                form.MinNumberOfAttendances = request.MinNumberAttendances;

                unitOfWork.FormRepository.Update(form);
                await unitOfWork.SaveChangesAsync();

                string startDate = request.StartDate.ToString("yyyy-MM-dd hh:mm:ss");
                string endDate = request.EndDate.ToString("yyyy-MM-dd hh:mm:ss");
                string interval = string.Join(" - ", startDate, endDate);
                Notification notification = ConfigureNotificationMessage(interval);
                emailService.Send(notification);
            }
            else
            {
                throw new ItemNotFoundException("The form was not found...");
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
