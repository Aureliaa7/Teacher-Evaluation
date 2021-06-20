using TeacherEvaluation.EmailSender.NotificationModel;

namespace TeacherEvaluation.EmailSender.NotificationService.Interfaces
{
    public interface INotificationService
    {
        void Send(Notification emailMessage);
    }
}
