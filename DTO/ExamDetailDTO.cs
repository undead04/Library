namespace Library.DTO
{
    public class ExamDetailDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string NameSubject { get; set; }=string.Empty;
        public List<QuestionDetail>? questionDetails { get; set; }
    }
}
