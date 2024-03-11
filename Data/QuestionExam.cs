namespace Library.Data
{
    public class QuestionExam
    {
        public int Id { get;set; }
        public int Examid { get;set; }
        public int QuestionId { get; set; }
        public virtual Exam?Exam { get; set; }
        public virtual Question? Question { get; set; } 
    }
}
