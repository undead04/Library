namespace Library.Data
{
    public class NotificationSubject
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime Create_At { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        public ICollection<NotificationClassRoom>? notificationClassRooms { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
