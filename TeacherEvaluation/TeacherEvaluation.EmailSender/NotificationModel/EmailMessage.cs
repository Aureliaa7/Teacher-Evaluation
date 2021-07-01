using System.Collections.Generic;

namespace TeacherEvaluation.EmailSender.NotificationModel
{
    public class EmailMessage
    {
		public IList<EmailAddress> ToAddresses { get; set; } = new List<EmailAddress>();
		public EmailAddress FromAddress { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}
