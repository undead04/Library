namespace Library.Model.DTO
{
    public class ExamEssayDTO
    {
        public string NameSubject { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty;
        public List<ExamEssayDetailDTO>? ExamEssayDetail { get; set; }

    }
}
