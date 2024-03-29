namespace Library.Model.DTO
{
    public class ReplyDTO
    {
        public int Id { get; set; }
        public DateTime Create_at { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
    }
}
