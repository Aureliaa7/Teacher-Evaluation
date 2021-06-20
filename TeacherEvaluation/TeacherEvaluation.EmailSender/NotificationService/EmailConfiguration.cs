using TeacherEvaluation.EmailSender.NotificationService.Interfaces;

namespace TeacherEvaluation.EmailSender.NotificationService
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
