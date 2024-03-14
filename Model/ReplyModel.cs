namespace Library.Model
{
    public class ReplyModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public int QuestionId { get; set; }
    }
    public class ReplyUpdateModel
    {
        public string Context { get; set; } = string.Empty;
    }
}
