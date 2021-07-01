using TeacherEvaluation.Domain.Identity;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.EmailSender.NotificationService
{
    public class EmailSending
    {
        public static EmailMessage ConfigureAccountCreationMessage(string url, ApplicationUser newApplicationUser, string password)
        {
            EmailMessage notification = NotifyAccountCreation(url, newApplicationUser, password);

            EmailAddress recipient = GetRecipientAddress(newApplicationUser);
            notification.ToAddresses.Add(recipient);
            notification.FromAddress = new EmailAddress
            {
                Name = "noreply-teacher.evaluation",
                Address = "teacher.evaluation.project2021@gmail.com"
            };

            return notification;
        }

        private static EmailMessage NotifyAccountCreation(string url, ApplicationUser newApplicationUser, string password)
        {
            string bodyResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EmailConfirmationNotificationBody.txt";
            string subjectResourceName = "TeacherEvaluation.EmailSender.NotificationTemplates.EmailConfirmationNotificationSubject.txt";

            string notificationBody = ResourceProvider.GetResourceText(bodyResourceName);
            string notificationSubject = ResourceProvider.GetResourceText(subjectResourceName);
            string recipientName = GetRecipientAddress(newApplicationUser).Name;
            string loginPageUrl = url;
            string accountPassword = password;

            return new EmailMessage
            {
                Subject = notificationSubject,
                Content = string.Format(notificationBody, recipientName, loginPageUrl, accountPassword)
            };
        }

        private static EmailAddress GetRecipientAddress(ApplicationUser newApplicationUser)
        {
            ApplicationUser user = newApplicationUser;
            string name = string.Join(" ", newApplicationUser.LastName, newApplicationUser.FathersInitial, newApplicationUser.FirstName);
            return new EmailAddress
            {
                Name = name,
                Address = user.Email
            };
        }
    }
}
