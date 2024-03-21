namespace Library.DTO
{
    public class QuestionDetail
    {
        public string context { get; set; } = string.Empty;
        public List<AnswerDTO>?Answers { get; set; }
    }
}
