namespace Library.Data
{
    public class MyNotification
    {
        
        public string UserId { get; set; } = string.Empty;
        public int NotificationId { get; set; }
        public bool IsRead { get; set; } = false;
        public virtual ApplicationUser? User { get; set; }
        public virtual Notification? Notification { get; set; }

    }
}
