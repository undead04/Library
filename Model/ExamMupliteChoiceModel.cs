namespace Library.Model
{
    public class ExamMupliteChoiceModel
    {
        public int SubjectId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Time { get; set;} = string.Empty;
       public List<QuestionModel>? Question { get; set; }
       
    }
}
