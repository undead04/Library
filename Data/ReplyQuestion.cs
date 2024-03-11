namespace Library.Data
{
    public class ReplyQuestion
    {
        public int Id { get;set; }
        public int UserId { get; set; }
        public string Context { get; set; } = string.Empty;
        public DateTime Create_At { get; set; }
        public virtual QuestionSubject? QuestionSubject { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
