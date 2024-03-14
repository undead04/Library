namespace Library.Data
{
    public class Exam
    {
        public int Id { get; set; }
        public string Form { get; set; } = string.Empty;
        public string Name { get; set; }=string.Empty;
        public string Time { get; set; }=string.Empty;
        public string UserId { get; set; }=string.Empty;
        public DateTime Create_At { get; set; }
        public string Status { get; set; }=string.Empty;
        public ICollection<EssayExam>? essayExams { get; set; }
        public virtual ApplicationUser? applicationUsers { get; set; }
        public ICollection<QuestionExam>?QuestionExams { get; set; }

    }
}
