using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.EmailSender.NotificationService
{
    public class EmailService : INotificationService
    {
		private readonly IEmailConfiguration emailConfiguration;
		public EmailService(IEmailConfiguration emailConfiguration)
		{
			this.emailConfiguration = emailConfiguration;
		}

		public void Send(Notification notification)
		{
			var message = new MimeMessage();
			message.To.AddRange(notification.ToAddresses.Select(recipient => new MailboxAddress(recipient.Name, recipient.Address)));
			message.From.Add(new MailboxAddress(notification.FromAddress.Name, notification.FromAddress.Address));

			message.Subject = notification.Subject;

			message.Body = new TextPart("plain")
			{
				Text = notification.Content
			};

			using(SmtpClient emailClient = new SmtpClient())
			{
				var smtpServer = emailConfiguration.SmtpServer;
				var port = emailConfiguration.SmtpPort;
				var username = emailConfiguration.SmtpUsername;
				var password = emailConfiguration.SmtpPassword;

				emailClient.Connect(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, true);
				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
				emailClient.Authenticate(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
				emailClient.Send(message);
				emailClient.Disconnect(true);
			}
		}
	}
}
