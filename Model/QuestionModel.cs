namespace Library.Model
{
    public class QuestionModel
    {
        public string CreateUserId { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public string Context { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public AnswerModel[] answers { get; set; }=new AnswerModel[4];
    }
}
