namespace Library.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string CreateUserId { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public string Context { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string AnswerType { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        public ICollection<QuestionExam>? questionExams { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public ICollection<Answer>? Answers { get; set; }
       
    }
}
