namespace Library.Data
{
    public class ReplyQuestion
    {
        public int Id { get;set; }
        public string UserId { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public DateTime Create_At { get; set; }
        public int QuestionId { get; set; }
        public virtual QuestionSubject? QuestionSubject { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
