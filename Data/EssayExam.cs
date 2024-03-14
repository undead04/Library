namespace Library.Data
{
    public class EssayExam
    {
        public int Id { get;set; }
        public int ExamId { get; set; }
        public string Context { get; set; } = string.Empty;
        public virtual Exam? Exam { get; set; }
    }
}
