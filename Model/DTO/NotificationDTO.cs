namespace Library.Model.DTO
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string urlImage { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
    }
}
