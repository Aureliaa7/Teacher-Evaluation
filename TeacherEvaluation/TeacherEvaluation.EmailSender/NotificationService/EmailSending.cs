using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.EmailSender.NotificationService
{
    public class EmailSending
    {
        public static Notification ConfigureNotificationMessage(string url, ApplicationUser newApplicationUser, string password)
        {
            Notification notification = CreateNotificationMessage(url, newApplicationUser, password);

            NotificationAddress recipient = GetRecipientAddress(newApplicationUser);
            notification.ToAddresses.Add(recipient);
            notification.FromAddress = new NotificationAddress("noreply-teacher.evaluation", "teacher.evaluation.project2021@gmail.com");

            return notification;
        }

        private static Notification CreateNotificationMessage(string url, ApplicationUser newApplicationUser, string password)
        {
            string bodyResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EmailConfirmationNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EmailConfirmationNotificationSubject.txt";

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

        private static NotificationAddress GetRecipientAddress(ApplicationUser newApplicationUser)
        {
            ApplicationUser user = newApplicationUser;
            string name = string.Join(" ", newApplicationUser.LastName, newApplicationUser.FathersInitial, newApplicationUser.FirstName);
            return new NotificationAddress(name, user.Email);
        }
    }
}
