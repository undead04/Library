namespace Library.Model
{
    public class ExamEssayModel
    {
        public string UserId { get; set; } = string.Empty;
       
        public string Name { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string Time { get; set; } = string.Empty;
        public List<QuestionEssay>? Context { get; set; }
    }
}
