namespace Library.Model.DTO
{
    public class ExamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
