using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using System.Threading.Tasks;
using TeacherEvaluation.EmailSender.NotificationModel;
using TeacherEvaluation.EmailSender.NotificationService.Interfaces;

namespace TeacherEvaluation.EmailSender.NotificationService
{
    public class EmailService : IEmailService
    {
		private readonly IEmailConfiguration emailConfiguration;
		public EmailService(IEmailConfiguration emailConfiguration)
		{
			this.emailConfiguration = emailConfiguration;
		}

		public async Task SendAsync(EmailMessage notification)
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

				// establish a connection to the specified mail server
				await emailClient.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, true);
				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
				await emailClient.AuthenticateAsync(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
				await emailClient.SendAsync(message);
				// disconnect the service
				await emailClient.DisconnectAsync(true);
			}
		}
	}
}
