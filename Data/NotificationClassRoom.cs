namespace Library.Data
{
    public class NotificationClassRoom
    {
        public int Id { get; set; }
        public int SubjectNotificationid { get; set; }
        public int ClassRoomId { get; set; }
        public virtual ClassRoom? ClassRoom { get; set; }
        public virtual NotificationSubject? NotificationSubject { get; set; }
    }
}
