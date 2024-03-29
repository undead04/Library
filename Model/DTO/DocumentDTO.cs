namespace Library.Model.DTO
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Classify { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public string Note { get; set; } = string.Empty;
        public string StatusDocument { get; set; } = string.Empty;
        public DateTime CancelDate { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string UrlFile { get; set; } = string.Empty;
    }
}
