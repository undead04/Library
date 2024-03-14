namespace Library.DTO
{
    public class QuestionSubjectDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; }=string.Empty;
        public string Title { get; set; } = string.Empty;
        public string context { get; set; } = string.Empty;
        public int Like { get; set; }
        public DateTime Create_at { get; set; }
    }
}
