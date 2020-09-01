using System.Collections.Generic;

namespace TeacherEvaluation.EmailSender.NotificationModel
{
    public class Notification
    {
		public List<NotificationAddress> ToAddresses { get; set; }
		public NotificationAddress FromAddress { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }

		public Notification()
		{
			ToAddresses = new List<NotificationAddress>();
			FromAddress = new NotificationAddress();
		}
	}
}
