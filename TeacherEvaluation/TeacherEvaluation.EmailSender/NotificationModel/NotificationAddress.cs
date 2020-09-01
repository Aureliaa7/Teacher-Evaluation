namespace TeacherEvaluation.EmailSender.NotificationModel
{
    public class NotificationAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public NotificationAddress() { }
        public NotificationAddress(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
