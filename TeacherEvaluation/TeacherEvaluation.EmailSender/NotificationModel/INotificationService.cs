namespace TeacherEvaluation.EmailSender.NotificationModel
{
    public interface INotificationService
    {
        void Send(Notification emailMessage);
    }
}
