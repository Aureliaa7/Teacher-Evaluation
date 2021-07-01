using System.Threading.Tasks;
using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.EmailSender.NotificationService.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage emailMessage);
    }
}
