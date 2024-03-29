namespace Library.Data
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string Content { get; set; } = string.Empty;
        public string TypeNotification { get; set; }= string.Empty;
        public ApplicationUser ? applicationUser { get;set; }
        public ICollection<MyNotification>? myNotifications { get; set; }


    }
}
